namespace AlphaX.CalcEngine.Evaluator;

public class CalcError(string msg, string err)
{
    public string Message { get; private set; } = msg;
    public string Error { get; private set; } = err;
}

public class ValueError: CalcError
{
    public ValueError(): base("A value used in the formula is not of correct data type", "#Value!")
    {
        
    }
}

public class NameError : CalcError
{
    public NameError() : base("The formula contains unrecognized text", "#Name!")
    {

    }
}

public class DivideByZeroError : CalcError
{
    public DivideByZeroError() : base("Trying to divide value by 0", "#Div/0!")
    {

    }
}
