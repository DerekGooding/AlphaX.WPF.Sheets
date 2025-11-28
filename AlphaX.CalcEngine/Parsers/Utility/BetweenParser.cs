using AlphaX.CalcEngine.Parsers.Base;

namespace AlphaX.CalcEngine.Parsers.Utility;

internal class BetweenParser(Parser left, Parser right, Parser content) : SequenceOfParser(new Parser[]{ left, content, right})
{
    public override ParserState Parse(ParserState state)
    {
        var nextState = base.Parse(state);

        return nextState.IsError ? nextState : UpdateResult(nextState, (nextState.Result as ArrayResult).Value[1]);
    }
}
