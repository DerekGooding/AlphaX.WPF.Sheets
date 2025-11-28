using System.Text.RegularExpressions;

namespace AlphaX.CalcEngine.Parsers.Calc;

internal partial class CellRef
{
    public string Name => string.IsNullOrEmpty(SheetName) ? field : SheetName + "!" + field;
    public int Column { get; }
    public int Row { get; }
    public string SheetName { get; }

    public CellRef(string name)
    {

        string cellRef = name, sheetName = "";
        if (name.Contains('!'))
        {
            var temp = name.Split('!');
            cellRef = temp[1];
            sheetName = temp[0];
        }

        var res = MyRegex().Split(cellRef).Where(r => r.Length > 0);
        Name = cellRef;
        Column = GetColumnNumberFromLetter(res.ElementAt(0)) - 1;
        Row = int.Parse(res.ElementAt(1)) - 1;
        SheetName = sheetName;
    }

    public CellRef(int row, int col, string sheetName = "")
    {
        Row = row;
        Column = col;
        Name = GetColumnLetterFromNumber(col + 1) + (row + 1).ToString();
        SheetName = sheetName;
    }

    private int GetColumnNumberFromLetter(string letter)
    {
        letter = letter.ToUpperInvariant();
        return letter.Length == 1 ? letter[0] - 64
            : (26 * GetColumnNumberFromLetter(letter[..1])) + GetColumnNumberFromLetter(letter[1..]);
    }

    private string GetColumnLetterFromNumber(int num)
        => num < 27 ? ((char)(num + 64)).ToString() : GetColumnLetterFromNumber(num / 26) + GetColumnLetterFromNumber(num % 26);

    #region equals comparion

    public override bool Equals(object? obj) => Equals(obj as CellRef);

    public bool Equals(CellRef cRef)
    {
        if (cRef is null)
        {
            return false;
        }

        // Optimization for a common success case.
        if (Object.ReferenceEquals(this, cRef))
        {
            return true;
        }

        // If run-time types are not exactly the same, return false.
        return GetType() == cRef.GetType() && Name == cRef.Name;
    }

    public override int GetHashCode() => Name.GetHashCode();

    public static bool operator ==(CellRef lhs, CellRef rhs)
    {
        if (lhs is null)
        {
            if (rhs is null)
            {
                return true;
            }

            // Only the left side is null.
            return false;
        }
        // Equals handles case of null on right side.
        return lhs.Equals(rhs);
    }

    public static bool operator !=(CellRef lhs, CellRef rhs) => !(lhs == rhs);

    [GeneratedRegex(@"(\d+)")]
    private static partial Regex MyRegex();

    #endregion
}
