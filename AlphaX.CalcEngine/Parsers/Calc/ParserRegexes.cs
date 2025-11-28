namespace AlphaX.CalcEngine.Parsers.Calc;

internal static class ParserRegexes
{
    public static string GetFloatParserRegex() => @"^\d*\.\d+";

    public static string GetFormulaParserRegex() => @"^\s*,\s*";

    public static string GetVarParserRegex() => @"^[a-zA-Z]+[\w\d]*";
}
