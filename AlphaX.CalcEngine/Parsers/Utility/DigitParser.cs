using AlphaX.CalcEngine.Parsers.Base;
using System.Text.RegularExpressions;

namespace AlphaX.CalcEngine.Parsers.Utility;

internal class DigitParser : RegexParser
{
    static string _digitsRegex = "^-?[0-9]+";

    public DigitParser(): base(new Regex(_digitsRegex))
    {
        
    }

    protected override ParserError InternalErrorMap(ParserState state, ParserError error) => new($"Error at index {state.Index}, Expected digits");
}
