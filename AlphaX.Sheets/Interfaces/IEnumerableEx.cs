namespace AlphaX.Sheets.Interfaces;

public interface IEnumerableEx<T>
{
    /// <summary>
    /// Enumerates the items present in this collection.
    /// </summary>
    /// <returns></returns>
    IEnumerable<T> Enumerate();
}
