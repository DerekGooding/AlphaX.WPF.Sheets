namespace AlphaX.WPF.Sheets.UI.Managers;

internal abstract class UIManager(AlphaXSpread spread) : IDisposable
{
    protected AlphaXSpread Spread { get; private set; } = spread;

    public void Dispose() => Spread = null;
}
