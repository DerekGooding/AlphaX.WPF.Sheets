using AlphaX.CalcEngine.Parsers.Base;

namespace AlphaX.CalcEngine.Parsers.Utility;

internal class SequenceOfParser(Parser[] parsers) : Parser
{
    private Parser[] _parsers = parsers;

    public override ParserState Parse(ParserState state)
    {
        if (state.IsError)
        {
            return state;
        }

        List<ParserResult> results = [];
        var nextState = state;

        foreach (var p in _parsers)
        {
            nextState = p.Parse(nextState);
            if (!nextState.IsError)
            {
                results.Add(nextState.Result);
            }
            else
            {
                return UpdateError(state, nextState.Error);
            }
        }

        return UpdateResult(nextState, new ArrayResult(results.ToArray()));
    }
}
