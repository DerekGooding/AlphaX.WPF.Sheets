namespace AlphaX.Sheets.Abstractions;

public abstract class CollectionBase<T> where T : class
{
    protected internal SortedDictionary<int, T> InternalCollection;
    protected abstract int Count { get; }

    public T? this[int index] => GetItem(index, true);

    public object Parent { get; }

    internal CollectionBase(object parent)
    {
        Parent = parent;
        InternalCollection = [];
    }

    internal abstract double GetLocation(int index, bool recalculate = false);
    public abstract void InsertBelow(int index);
    public abstract void InsertBelow(int index, int count);
    public abstract void Remove(int index);
    public abstract void Remove(int index, int count);
    public abstract void Add();
    public abstract void Add(int count);

    public IEnumerable<T> Enumerate()
    {
        for (var row = 0; row < Count; row++)
        {
            yield return GetItem(row, true);
        }
    }

    /// <summary>
    /// Enumerates the actual items present in the collection.
    /// </summary>
    /// <returns></returns>
    public IEnumerable<T> EnumerateInternal()
    {
        foreach (var item in InternalCollection)
            yield return item.Value;
    }

    /// <summary>
    /// Gets the item present at the specified index.
    /// </summary>
    /// <param name="index">
    /// Index of the item.
    /// </param>
    /// <param name="createIfNotExist">
    /// Whether to create and add the item if not exist.
    /// </param>
    public T? GetItem(int index, bool createIfNotExist) =>
        InternalCollection.TryGetValue(index, out var item) ? item : createIfNotExist ? AddItemInternal(index) : null;

    /// <summary>
    /// Validates whether the provided index was out of range
    /// </summary>
    /// <param name="index"></param>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    private void ValidateIndex(int index)
    {
        if (index < 0 || index >= Count)
            throw new ArgumentOutOfRangeException(nameof(index));
    }

    /// <summary>
    /// Adds a new item of type T at the provided index.
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    protected T AddItemInternal(int index)
    {
        var item = CreateItem(index);
        InternalCollection.Add(index, item);
        return item;
    }

    /// <summary>
    /// Creates a new item.
    /// </summary>
    /// <returns></returns>
    protected abstract T CreateItem(int index);
}
