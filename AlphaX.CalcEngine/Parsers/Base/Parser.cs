namespace AlphaX.CalcEngine.Parsers.Base;

internal abstract class Parser
{
    // main parser method
    public virtual ParserState Parse(ParserState state) => state.Clone();

    // initial state provider
    public ParserState Run(string inputString)
    {
        var initialState = new ParserState { 
            InputString = inputString
        };
        return Parse(initialState);
    }

    // map result to another form
    public Parser Map(MapDelegate mapFn) => new MappedParser(this, mapFn);

    // map error to another form
    public Parser MapError(ErrorMapDelegate errorMapFn) => new MappedParser(this, null, errorMapFn);

    // chain parser result to another parser
    public Parser Chain(ChainDelegate chainFn) => new ChainedParser(this, chainFn);

    // override in inherited class to return custom result and error
    protected virtual ParserResult InternalResultMap(ParserState state, int index, ParserResult result) => result;

    protected virtual ParserError InternalErrorMap(ParserState state, ParserError error) => error;

    // state modifiers, immutable
    public ParserState UpdateState(ParserState state, int index, ParserResult result)
    {
        var newState = state.Clone();
        newState.Index = index;
        newState.Result = InternalResultMap(state, index, result);

        return newState;
    }

    public ParserState UpdateResult(ParserState state, ParserResult result)
    {
        var newState = state.Clone();
        newState.Result = InternalResultMap(state, state.Index, result);

        return newState;
    }

    public ParserState UpdateError(ParserState state, ParserError error)
    {
        var newState = state.Clone();
        newState.Error = InternalErrorMap(state, error);
        newState.IsError = true;
        return newState;
    }
}
