namespace AlphaX.CalcEngine.Parsers.Base;

internal delegate Parser ChainDelegate(ParserResult result);

internal class ChainedParser(Parser parser, ChainDelegate chainFn) : Parser
{
    private Parser Parser { get; set; } = parser;
    private ChainDelegate ChainFn { get; set; } = chainFn;

    public override ParserState Parse(ParserState state)
    {
        var newState = Parser.Parse(state);
        if (newState.IsError)
        {
            return newState;
        }

        var newParser = ChainFn(newState.Result);
        return newParser.Parse(newState);
    }
}
