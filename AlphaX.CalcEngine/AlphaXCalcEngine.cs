using AlphaX.CalcEngine.Evaluator;
using AlphaX.CalcEngine.Formulas;
using AlphaX.CalcEngine.Interfaces;
using AlphaX.CalcEngine.Parsers.Base;
using AlphaX.CalcEngine.Parsers.Calc;

namespace AlphaX.CalcEngine;

public delegate void CellRecalculatedEventHandler(int row, int column);

public class AlphaXCalcEngine : ICalcEngine
{
    private readonly CalcParser _parser;
    private readonly CalcEvaluator _evaluator;
    private readonly IDataProvider _provider;
    public event CellRecalculatedEventHandler CellRecalculated;

    public AlphaXCalcEngine(IDataProvider dataProvider)
    {
        _parser = new CalcParser();
        _evaluator = new CalcEvaluator(dataProvider);
        _provider = dataProvider;
        _provider.ValueChanged += CellValueChanged;
    }

    private void CellValueChanged(ValueChangedEventArgs args) => UpdateDependents(args.SheetName, args.Row, args.Column);

    // eval single formula string
    public CalcValue EvaluateExpression(string str, string sheetName = "")
    {
        var ast = _parser.Run(str);
        return _evaluator.EvaluateExpressionTree(ast, sheetName);
    }

    // get list of all registered formulas
    public IEnumerable<Formula> GetRegisteredFormulas() => _evaluator.GetRegisteredFormulas();

    // register a custom formula
    public void RegisterCustomFormula(Formula formula) => _evaluator.RegisterFormula(formula);

    // get computed value of a formula
    public object GetValue(string sheetName, int row, int column)
    {
        if (_provider.GetMetaData(sheetName, row, column) is not CalcCellMetaInfo metaInfo || string.IsNullOrEmpty(metaInfo.Formula))
        {
            return null;
        }

        // value not calculated yet, calculate value
        metaInfo.CalculatedValue ??= _evaluator.EvaluateExpressionTree(metaInfo.CalcChain, sheetName);

        return metaInfo.CalculatedValue;
    }

    // get formula set in cell
    public string GetFormula(string sheetName, int row, int column)
    {
        var metaInfo = _provider.GetMetaData(sheetName, row, column) as CalcCellMetaInfo;
        return metaInfo?.Formula;
    }

    // set formula in sheet
    public void SetFormula(string sheetName, int row, int column, string formula)
    {
        var curFormula = GetFormula(sheetName, row, column);
        // same as existing formula, no need to update
        if (curFormula == formula)
        {
            return;
        }
        SetFormulaInternal(sheetName, row, column, formula);
    }

    // Get ranges
    public List<object> GetRangesInFormulaString(string formula)
    {
        var ranges = new List<object>();

        var parseResult = ParserProvider.RangeFinderParser.Run(formula);
        var foundRanges = (parseResult.Result as ArrayResult).Value;

        for (var i = 0; i < foundRanges.Length; i++)
        {
            ranges.Add((foundRanges[i] as CalcParserResult).ComputedValue);
        }

        return ranges;
    }

    #region private

    // set formula in sheet
    private void SetFormulaInternal(string sheetName, int row, int column, string formula)
    {

        if (string.IsNullOrEmpty(formula))
        {
            ClearFormula(sheetName, row, column);
            return;
        }

        var dependents = GetDependentsSet(sheetName, row, column);

        var metaInfo = new CalcCellMetaInfo()
        {
            Formula = formula,
            Dependents = dependents
        };
        CalcParserResult ast;
        try
        {
            ast = _parser.Run(formula);
            metaInfo.CalcChain = ast;
        }
        catch (Exception)
        {
            throw new CalcEngineException("Invalid formula");
        }


        var dependencies = new List<object>();
        _evaluator.FillDependencies(ast, dependencies);
        metaInfo.Dependencies = dependencies;

        var curCell = new CellRef(row, column, sheetName);

        if (dependencies.Count > 0)
        {
            foreach (var dependency in dependencies)
            {
                if (dependency is CellRef)
                {
                    SetCellDependency(curCell, dependency as CellRef);
                }
                else
                {
                    SetRangeDependency(curCell, dependency as CellRangeRef);
                }
            }
        }

        _provider.SetMetaData(sheetName, row, column, metaInfo);
        RecalculateCell(sheetName, row, column);
    }

    // remove formula from a cell(if present)
    private void ClearFormula(string sheetName, int row, int column)
    {
        if (_provider.GetMetaData(sheetName, row, column) is CalcCellMetaInfo metaInfo)
        {
            metaInfo.Formula = null;
            metaInfo.Dependencies.Clear();
            metaInfo.CalcChain = null;
            metaInfo.CalculatedValue = null;
        }

        UpdateDependents(sheetName, row, column);
    }

    // add a cell as dependent on other cell
    private void SetCellDependency(CellRef dependentCell, CellRef targetCell)
    {
        if (string.IsNullOrEmpty(targetCell.SheetName))
        {
            targetCell = new CellRef(targetCell.Row, targetCell.Column, dependentCell.SheetName);
        }

        var dependentSet = GetDependentsSet(targetCell.SheetName, targetCell.Row, targetCell.Column, true);

        dependentSet.Add(dependentCell);

    }

    // add a cell as dependent on a cell range
    private void SetRangeDependency(CellRef dependentCell, CellRangeRef targetRange)
    {
        for (var r = targetRange.TopRow; r <= targetRange.BottomRow; r++)
        {
            for (var c = targetRange.LeftColumn; c <= targetRange.RightColumn; c++)
            {
                SetCellDependency(dependentCell, new CellRef(r, c, targetRange.SheetName));
            }
        }
    }

    // get dependents set of a cell, optionally create empty dependents set not present
    private HashSet<CellRef> GetDependentsSet(string sheetName, int row, int column, bool createEmptySetIfNull = false)
    {
        var metaInfo = _provider.GetMetaData(sheetName, row, column) as CalcCellMetaInfo;

        if (metaInfo == null && createEmptySetIfNull)
        {
            metaInfo = new CalcCellMetaInfo()
            {
                Dependents = new HashSet<CellRef>()
            };
            _provider.SetMetaData(sheetName, row, column, metaInfo);

            return (HashSet<CellRef>)metaInfo.Dependents;
        }
        else if (metaInfo != null && metaInfo.Dependents == null && createEmptySetIfNull)
        {
            metaInfo.Dependents = new HashSet<CellRef>();
            return (HashSet<CellRef>)metaInfo.Dependents;
        }
        else if (metaInfo != null)
        {
            return metaInfo.Dependents as HashSet<CellRef>;
        }

        return null;
    }

    // get list of all the cells dependent on a cell traversing the complete calc chain
    private IList<CellRef> GetDependentCells(string sheetName, int row, int column)
    {
        var dependents = new List<CellRef>();
        var dependentsSetQueue = new Queue<HashSet<CellRef>>();
        var dependentSet = GetDependentsSet(sheetName, row, column);
        if (dependentSet != null)
        {
            dependentsSetQueue.Enqueue(dependentSet);
        }

        while (dependentsSetQueue.Count > 0)
        {
            var en = dependentsSetQueue.Dequeue().GetEnumerator();
            while (en.MoveNext())
            {
                dependents.Add(en.Current);
                dependentSet = GetDependentsSet(en.Current.SheetName, en.Current.Row, en.Current.Column);
                if (dependentSet != null)
                {
                    dependentsSetQueue.Enqueue(dependentSet);
                }
            }
        }

        return dependents;
    }

    // recalculate value of a cell
    private void RecalculateCell(string sheetName, int row, int column)
    {
        UpdateCellValue(sheetName, row, column);

        // update dependent cells
        UpdateDependents(sheetName, row, column);
    }

    // private update dependents
    private void UpdateDependents(string sheetName, int row, int column)
    {
        // update dependent cells, first clear value, then recalc
        var dependents = GetDependentCells(sheetName, row, column);
        for (var i = 0; i < dependents.Count; i++)
        {
            if (_provider.GetMetaData(dependents[i].SheetName, dependents[i].Row, dependents[i].Column) is CalcCellMetaInfo metaInfo)
            {
                metaInfo.CalculatedValue = null;
            }
        }

        // disable calc on demand
        foreach (var dependent in dependents)
        {

            UpdateCellValue(dependent.SheetName, dependent.Row, dependent.Column);
        }

        CellRecalculated?.Invoke(row, column);
    }

    // private void update cell value
    private void UpdateCellValue(string sheetName, int row, int column)
    {
        if (_provider.GetMetaData(sheetName, row, column) is not CalcCellMetaInfo metaInfo || string.IsNullOrEmpty(metaInfo.Formula))
        {
            return;
        }

        // has formula, update value
        var val = _evaluator.EvaluateExpressionTree(metaInfo.CalcChain, sheetName);
        metaInfo.CalculatedValue = val ?? new CalcValue() { Kind = CalcValueKind.Number, Value = 0 };
    }

    #endregion
}
