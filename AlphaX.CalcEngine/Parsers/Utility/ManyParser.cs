using AlphaX.CalcEngine.Parsers.Base;

namespace AlphaX.CalcEngine.Parsers.Utility;

internal class ManyParser(Parser parser, int minCount = 0) : Parser
{
    private Parser _parser = parser;
    private int _minCount = minCount;

    public override ParserState Parse(ParserState state)
    {
        if (state.IsError)
        {
            return state;
        }

        List<ParserResult> results = [];
        var nextState = state;

        while (!nextState.IsError)
        {
            nextState = _parser.Parse(nextState);
            if (!nextState.IsError)
            {
                results.Add(nextState.Result);
                state = nextState;
            }
        }

        return results.Count < _minCount
            ? UpdateError(state, new ParserError($"expected {_minCount} counts, but got {results.Count} counts"))
            : UpdateResult(state, new ArrayResult(results.ToArray()));
    }

}

internal class ManyOneParser(Parser parser) : ManyParser(parser, 1)
{
}

internal class ManySeptParser(Parser parser, Parser septBy, int minCount = -1) : Parser
{
    private Parser Parser { get; set; } = parser;
    private Parser SeptByParser { get; set; } = septBy;

    private int MinCount = minCount;

    public override ParserState Parse(ParserState state)
    {
        if (state.IsError)
        {
            return state;
        }

        List<ParserResult> results = [];
        var nextState = state;

        while (!nextState.IsError)
        {
            nextState = Parser.Parse(nextState);
            if (!nextState.IsError)
            {
                results.Add(nextState.Result);
                state = nextState;
            }

            nextState = SeptByParser.Parse(nextState);
        }

        return results.Count < MinCount
            ? UpdateError(state, new ParserError($"expected {MinCount} counts, but got {results.Count} counts"))
            : UpdateResult(state, new ArrayResult(results.ToArray()));

    }
}

internal class ManyMaxParser(Parser parser, int minCount = 0, int maxCount = 1) : Parser
{
    private Parser _parser = parser;
    private int _minCount = minCount;
    private int _maxCount = maxCount;

    public override ParserState Parse(ParserState state)
    {
        if (state.IsError)
        {
            return state;
        }

        List<ParserResult> results = [];
        var nextState = state;

        while (!nextState.IsError)
        {
            nextState = _parser.Parse(nextState);
            if (!nextState.IsError)
            {
                results.Add(nextState.Result);
                state = nextState;
            }
        }

        return results.Count < _minCount
            ? UpdateError(state, new ParserError($"expected min {_minCount} counts, but got {results.Count} counts"))
            : results.Count > _maxCount
                ? UpdateError(state, new ParserError($"expected max {_minCount} counts, but got {results.Count} counts"))
                : UpdateResult(state, new ArrayResult(results.ToArray()));
    }

}

internal class ManyOccuranceParser(Parser parser) : Parser
{
    private Parser Parser { get; set; } = parser;

    public override ParserState Parse(ParserState state)
    {
        if (state.IsError)
        {
            return state;
        }

        List<ParserResult> results = [];
        

        while (state.Index < state.InputString.Length)
        {
            var nextState = Parser.Parse(state);
            if (nextState.IsError)
            {
                state = state.Clone();
                state.Index++;
            }else
            {
                state = nextState;
                results.Add(nextState.Result);
            }

        }

        return UpdateResult(state, new ArrayResult(results.ToArray()));

    }
}
