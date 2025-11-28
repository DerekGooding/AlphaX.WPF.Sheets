using AlphaX.CalcEngine.Parsers.Base;
using System.Text.RegularExpressions;

namespace AlphaX.CalcEngine.Parsers.Utility;

internal partial class LettersParser : RegexParser
{
    const string _lettersRegex = "^[a-zA-Z]+";

    public LettersParser() : base(MyRegex())
    {

    }

    protected override ParserError InternalErrorMap(ParserState state, ParserError error)
        => new($"Error at index {state.Index}, Expected letters");
    [GeneratedRegex(_lettersRegex)]
    private static partial Regex MyRegex();
}
