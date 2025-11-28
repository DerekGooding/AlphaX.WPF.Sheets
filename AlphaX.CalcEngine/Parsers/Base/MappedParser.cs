namespace AlphaX.CalcEngine.Parsers.Base;

internal delegate ParserResult MapDelegate(ParserResult input);
internal delegate ParserError ErrorMapDelegate(ParserError input);

internal class MappedParser(Parser parser, MapDelegate mapFn = null, ErrorMapDelegate errorMapFn = null) : Parser
{

    private Parser Parser { get; } = parser;
    private MapDelegate MapFn { get; } = mapFn;
    private ErrorMapDelegate ErrorMapFn { get; } = errorMapFn;

    public override ParserState Parse(ParserState state)
    {
        var newState = Parser.Parse(state);
        return newState.IsError ? UpdateError(newState, newState.Error) : UpdateResult(newState, newState.Result);
    }

    protected override ParserResult InternalResultMap(ParserState state, int index, ParserResult result) => MapFn == null ? base.InternalResultMap(state, index, result) : MapFn(result);

    protected override ParserError InternalErrorMap(ParserState state, ParserError error) => ErrorMapFn == null ? base.InternalErrorMap(state, error) : ErrorMapFn(error);
}
