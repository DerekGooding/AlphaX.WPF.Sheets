using AlphaX.WPF.Sheets.UndoRedo.Actions;

namespace AlphaX.WPF.Sheets.UndoRedo;

public class UndoRedoManager(AlphaXSpread spread)
{
    private Stack<SheetAction> _undoStack = new();
    private Stack<SheetAction> _redoStack = new();
    private AlphaXSpread _spread = spread;

    public void AddAction(SheetAction action)
    {
        _undoStack.Push(action);

        if(_redoStack.Count > 0)
            _redoStack.Clear();
    }

    public void Redo()
    {
        if (_redoStack.Count > 0)
        {
            var action = _redoStack.Pop();
            action.Redo();
            _undoStack.Push(action);
        }
    }

    public void Undo()
    {
        if (_undoStack.Count > 0)
        {
            var action = _undoStack.Pop();
            action.Undo();
            _redoStack.Push(action);
        }
    }
}
