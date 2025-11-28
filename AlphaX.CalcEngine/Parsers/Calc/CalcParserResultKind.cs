namespace AlphaX.CalcEngine.Parsers.Calc;

public enum CalcParserResultKind
{
    Number,
    Float,
    VarName,
    CellRef,
    CellRangeRef,
    Operator,
    OpenParan,
    CloseParan,
    Formula,
    String,
    Bool
}
