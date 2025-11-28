using AlphaX.CalcEngine.Parsers.Base;
using System.Text.RegularExpressions;

namespace AlphaX.CalcEngine.Parsers.Utility;

internal partial class DigitParser : RegexParser
{
    const string _digitsRegex = "^-?[0-9]+";

    public DigitParser() : base(MyRegex())
    {

    }

    protected override ParserError InternalErrorMap(ParserState state, ParserError error)
        => new($"Error at index {state.Index}, Expected digits");

    [GeneratedRegex(_digitsRegex)]
    private static partial Regex MyRegex();
}
