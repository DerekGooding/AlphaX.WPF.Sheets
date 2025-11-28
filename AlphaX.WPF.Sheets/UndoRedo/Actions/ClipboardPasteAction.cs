namespace AlphaX.WPF.Sheets.UndoRedo.Actions;

internal class ClipboardPasteAction : SheetAction
{
    public State OldState { get; }
    public State NewState { get; }
    public AlphaXSheetView SheetView { get; set; }

    public ClipboardPasteAction()
    {
        OldState = new State();
        NewState = new State();
    }

    public override void Redo() => Execute(NewState);

    public override void Undo() => Execute(OldState);

    private void Execute(State state)
    {
        var data = (object[,])state.Value;

        SheetView.Spread.WorkBook.UpdateProvider.SuspendUpdates = true;

        for (var row = 0; row < data.GetLength(0); row++)
        {
            for (var column = 0; column < data.GetLength(1); column++)
            {
                var value = data[row, column];
                SheetView.WorkSheet.Cells[state.Row + row, state.Column + column].Value = value;
            }
        }

        var selection = state.Selection;
        SheetView.ActiveRow = state.Row;
        SheetView.ActiveColumn = state.Column;
        SheetView.Spread.SelectionManager.SelectRange(selection.TopRow, selection.LeftColumn, selection.RowCount, selection.ColumnCount);
        SheetView.Spread.WorkBook.UpdateProvider.SuspendUpdates = false;
    }
}
