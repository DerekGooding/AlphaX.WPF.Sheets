namespace AlphaX.WPF.Sheets.UndoRedo.Actions;

public abstract class SheetAction
{
    public abstract void Redo();
    public abstract void Undo();
}
