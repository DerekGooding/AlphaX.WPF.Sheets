namespace AlphaX.WPF.Sheets.Commands;

public class CancelEditCommand(AlphaXSpread spread) : AlphaXSpreadCommand(spread)
{
    public override bool CanExecute(object parameter) => Spread.EditingManager.IsEditing;

    public override void Execute(object parameter) => Spread.EditingManager.EndEdit(false);
}
