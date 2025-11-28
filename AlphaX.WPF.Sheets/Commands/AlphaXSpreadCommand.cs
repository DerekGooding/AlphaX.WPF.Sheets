using System.Windows.Input;

namespace AlphaX.WPF.Sheets.Commands;

public class AlphaXSpreadCommand(AlphaXSpread spread) : ICommand
{
    public event EventHandler CanExecuteChanged
    {
        add { CommandManager.RequerySuggested += value; }
        remove { CommandManager.RequerySuggested -= value; }
    }

    public AlphaXSpread Spread { get; } = spread;

    public virtual bool CanExecute(object parameter) => false;

    public virtual void Execute(object parameter)
    {
        
    }
}
