using AlphaX.CalcEngine.Evaluator;

namespace AlphaX.CalcEngine.Formulas;

public abstract class Formula(string name)
{
    public int MinArgs { get; set; }
    public int MaxArgs { get; set; }
    public string Name { get; } = name;

    public abstract CalcValue Calculate(params CalcValue[] values);

    public abstract string GetDescription();
}
