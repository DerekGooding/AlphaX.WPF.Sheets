using AlphaX.CalcEngine.Evaluator;

namespace AlphaX.CalcEngine.Formulas;

public class AverageFormula : Formula
{
    public AverageFormula() : base("AVERAGE")
    {
        MinArgs = 1;
        MaxArgs = int.MaxValue;
    }
    public override CalcValue Calculate(params CalcValue[] values)
    {
        double sum = 0;
        double count = 0;
        for (var i = 0; i < values.Length; i++)
        {
            var calcValue = values[i];
            switch (calcValue.Kind)
            {
                case CalcValueKind.Array:
                    foreach (var numValue in (object[,])calcValue.Value)
                    {
                        if (numValue != null)
                        {
                            sum += Convert.ToDouble(numValue);
                            count++;
                        }
                    }
                    break;

                case CalcValueKind.Number:
                case CalcValueKind.Float:
                    if (calcValue.Value != null)
                    {
                        sum += Convert.ToDouble(calcValue.Value);
                        count++;
                    }
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
            Kind = CalcValueKind.Float,
            Value = sum / count
        };
    }

    public override string GetDescription() => "Returns the average (arithmetic mean) of its arguments";
}
