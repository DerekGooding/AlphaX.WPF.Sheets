using AlphaX.CalcEngine.Parsers.Base;

namespace AlphaX.CalcEngine.Parsers.Utility;

internal class StringParser(string value) : Parser
{
    public string Value { get; set; } = value;

    public override ParserState Parse(ParserState state)
    {

        if (state.IsError)
        {
            return state;
        }

        var str = state.InputString.Substring(state.Index);

        return str.Length < Value.Length
            ? UpdateError(state, new ParserError($"Unexpected end of input, expected ${Value}, found end of input"))
            : str.StartsWith(Value)
            ? UpdateState(state, state.Index + Value.Length, new StringResult(Value))
            : UpdateError(state, new ParserError ($"No match found, expected {Value} but got {str}" ));
    }
}
