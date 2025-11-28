using AlphaX.WPF.Sheets.Rendering.Renderers;

namespace AlphaX.WPF.Sheets.Rendering;

internal class RenderEngineCache
{
    private readonly Dictionary<Renderer, Dictionary<(int, int), Drawing>> _drawingStore;

    public RenderEngineCache() => _drawingStore = [];

    public void RegisterCacheType(Renderer type) => _drawingStore.Add(type, []);

    /// <summary>
    /// Adds drawing to cache.
    /// </summary>
    public void AddDrawing(Renderer cacheType, int row, int col, Drawing drawing) => _drawingStore[cacheType][(row, col)] = drawing;

    /// <summary>
    /// Clears cache.
    /// </summary>
    public void Clear()
    {
        foreach (var stores in _drawingStore)
        {
            stores.Value.Clear();
        }
        GC.Collect();
    }

    /// <summary>
    /// Gets the drawing object from cache if exists.
    /// </summary>
    public bool TryGetDrawing(Renderer cacheType, int row, int col, out Drawing drawing) => _drawingStore[cacheType].TryGetValue((row, col), out drawing);

    /// <summary>
    /// Removes the drawing object from cache
    /// </summary>
    public void RemoveFromCache(Renderer cacheType, int row, int col)
    {
        if (_drawingStore[cacheType].ContainsKey((row, col)))
        {
            cacheType.Drawing.Children.Remove(_drawingStore[cacheType][(row, col)]);
            _drawingStore[cacheType].Remove((row, col));
        }
    }
}