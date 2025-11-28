namespace AlphaX.Sheets.Interfaces;

public interface ICloneable<T>
{
    /// <summary>
    /// Creates a clone of <typeparamref name="T"/>.
    /// </summary>
    /// <returns></returns>
    T Clone();
}
