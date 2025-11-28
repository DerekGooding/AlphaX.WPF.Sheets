using AlphaX.CalcEngine.Evaluator;

namespace AlphaX.CalcEngine.Formulas;

public class SumFormula : Formula
{
    public SumFormula() : base("SUM")
    {
        MinArgs = 0;
        MaxArgs = int.MaxValue;
    }

    public override CalcValue Calculate(params CalcValue[] values)
    {
        double sum = 0;

        for (var i = 0; i < values.Length; i++)
            CalculateInternal(values[i], ref sum);

        return new CalcValue()
        {
            Kind = CalcValueKind.Float,
            Value = sum
        };
    }

    public override string GetDescription() => "Adds all the numbers in a range of cells";

    private void CalculateInternal(CalcValue calcValue, ref double result)
    {
        if (calcValue == null)
            return;

        switch (calcValue.Kind)
        {
            case CalcValueKind.Array:
                foreach (var numValue in (CalcValue[,])calcValue.Value)
                    CalculateInternal(numValue, ref result);
                break;

            case CalcValueKind.Number:
            case CalcValueKind.Float:
                result += Convert.ToDouble(calcValue.Value);
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

}
