using System;
using System.Windows.Input;

public class ActionCommand : ICommand
{
    private readonly Action<object> _action;
    private readonly Predicate<Object> _predicate;

    public ActionCommand(Action<Object> action) : this(action, null)
    {
    }

    public ActionCommand()
    {

    }

    public ActionCommand(Action<object> action, Predicate<object> predicate)
    {
        if (action == null)
        {
            throw new ArgumentNullException("action", "You must specify an Action<T>.");
        }
        _action = action;
        _predicate = predicate;
    }

    public bool CanExecute(object parameter)
    {
        if (_predicate == null)
        {
            return true;
        }
        return _predicate(parameter);
    }

    public void Execute(object parameter)
    {
        _action(parameter);
    }

    public void Execute()
    {
        Execute(null);
    }
    public event EventHandler CanExecuteChanged
    {
        add { CommandManager.RequerySuggested += value; }
        remove { CommandManager.RequerySuggested -= value; }
    }
}