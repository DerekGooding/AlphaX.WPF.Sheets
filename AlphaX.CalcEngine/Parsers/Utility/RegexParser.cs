using AlphaX.CalcEngine.Parsers.Base;
using System.Text.RegularExpressions;

namespace AlphaX.CalcEngine.Parsers.Utility;

internal class RegexParser(Regex regex) : Parser
{
    private Regex _regex = regex;

    public override ParserState Parse(ParserState state)
    {
        if (state.IsError)
        {
            return state;
        }

        string inp = state.InputString.Substring(state.Index);
        var match = _regex.Match(inp);

        return match.Success
            ? UpdateState(state, state.Index + match.Value.Length, new StringResult(match.Value))
            : UpdateError(state, new ParserError($"No match found, expected pattern {_regex}, found {state.InputString}"));
    }

}
