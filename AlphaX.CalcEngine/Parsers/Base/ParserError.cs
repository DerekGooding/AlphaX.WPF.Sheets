namespace AlphaX.CalcEngine.Parsers.Base;

internal class ParserError(string message)
{
    public string Message { get; set; } = message;
}
