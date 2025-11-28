using AlphaX.CalcEngine.Parsers.Base;
using AlphaX.CalcEngine.Parsers.Calc;
using System.Text.RegularExpressions;

namespace AlphaX.CalcEngine.Parsers.Utility;

internal class BoolValueParser : Parser
{
    private readonly Regex _regex;

    public BoolValueParser() => _regex = new Regex(ParserRegexes.GetVarParserRegex());

    public override ParserState Parse(ParserState state)
    {
        if (state.IsError)
        {
            return state;
        }

        var inp = state.InputString[state.Index..];
        var match = _regex.Match(inp);

        if (match.Success)
        {
            var val = match.Value.ToLowerInvariant();
            if (val is "true" or "false")
            {
                return UpdateState(state, state.Index + match.Value.Length, new StringResult(val));
            }
        }

        return UpdateError(state, new ParserError($"No match found, expected bool value, found {state.InputString}"));
    }

}
