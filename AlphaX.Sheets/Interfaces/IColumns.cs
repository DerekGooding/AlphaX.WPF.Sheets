namespace AlphaX.Sheets.Interfaces;

public interface IColumns : IEnumerableEx<Column>, IDisposable
{
    /// <summary>
    /// Gets the parent this collection belongs to.
    /// </summary>
    object Parent { get; }
    /// <summary>
    /// Gets the column present at the provided index.
    /// </summary>
    /// <param name="index">
    /// Column index.
    /// </param>
    Column this[int index] { get; }
    /// <summary>
    /// Gets the column with specific column name.
    /// </summary>
    Column this[string address] { get; }
    int GetColumnWidth(int column);
}
