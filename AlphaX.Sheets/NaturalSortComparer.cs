using System.Runtime.InteropServices;

namespace AlphaX.Sheets;

internal partial class NaturalSortComparer : IComparer<KeyValuePair<int, Cell>>
{
    [LibraryImport("shlwapi.dll", StringMarshalling = StringMarshalling.Utf16)]
    private static partial int StrCmpLogicalW(string s1, string s2);

    public int Compare(KeyValuePair<int, Cell> x, KeyValuePair<int, Cell> y)
        => StrCmpLogicalW(x.Value == null || x.Value.Value == null ? string.Empty : x.Value.Value.ToString(),
            y.Value == null || y.Value.Value == null ? string.Empty : y.Value.Value.ToString());
}
