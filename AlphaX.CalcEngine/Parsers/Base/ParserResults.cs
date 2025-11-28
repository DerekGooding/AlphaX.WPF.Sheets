namespace AlphaX.CalcEngine.Parsers.Base;

public class ParserResult { }

class StringResult(string value) : ParserResult
{
    public string Value { get; set; } = value;
}

class ArrayResult(ParserResult[] value) : ParserResult
{
    public ParserResult[] Value { get; set; } = value;
}