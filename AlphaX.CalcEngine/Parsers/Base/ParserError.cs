namespace AlphaX.CalcEngine.Parsers.Base;

internal class ParserError
{
    public string Message { get; set; }

    public ParserError(string message)
    {
        Message = message;
    }
}
