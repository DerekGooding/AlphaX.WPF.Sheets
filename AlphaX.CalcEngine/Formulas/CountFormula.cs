using AlphaX.CalcEngine.Evaluator;

namespace AlphaX.CalcEngine.Formulas;

public class CountFormula : Formula
{
    public CountFormula() : base("COUNT")
    {
        MinArgs = 0;
        MaxArgs = int.MaxValue;
    }

    public override CalcValue Calculate(params CalcValue[] values)
    {
        var count = 0;
        for (var i = 0; i < values.Length; i++)
        {
            var calcValue = values[i];
            switch (calcValue.Kind)
            {
                case CalcValueKind.Array:
                    foreach (var numValue in (object[,])calcValue.Value)
                    {
                        if (numValue != null)
                            count++;
                    }
                    break;

                case CalcValueKind.Number:
                case CalcValueKind.Float:
                    if (calcValue.Value != null)
                        count++;
                    break;

                case CalcValueKind.String:
                    break;

                case CalcValueKind.Date:
                    break;

                case CalcValueKind.Error:
                    break;

                default:
                    throw new CalcEngineException("Invalid argument for formula.");
            }
        }

        return new CalcValue()
        {
            Kind = CalcValueKind.Number,
            Value = count
        };
    }

    public override string GetDescription() => "Counts the number of cells in a range that contains numbers";
}
