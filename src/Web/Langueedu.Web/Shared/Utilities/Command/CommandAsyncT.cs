using System.Windows.Input;

namespace Langueedu.Web.Shared.Utilities;


public class CommandAsync<T> : ICommand
{
  private readonly Func<T, Task> _execute;

  public CommandAsync(Func<T, Task> execute)
  {
    _execute = execute;
  }

  public CommandAsync(Func<Task> execute) : this(o => execute())
  {
    if (execute == null)
      throw new ArgumentNullException(nameof(execute));
  }

  public event EventHandler CanExecuteChanged;

  public bool CanExecute(object parameter)
  {
    return _execute != null;
  }

  public void Execute(object parameter)
  {
    if (CanExecute(parameter))
    {
      _execute.Invoke(parameter is T
                      ? (T)parameter
                      : default);

      if (CanExecuteChanged != null)
      {
        CanExecuteChanged.Invoke(parameter is T
                    ? (T)parameter
                    : default, null);
      }
    }
  }
}

public class CommandAsync : CommandAsync<object>
{
  public CommandAsync(Func<Task> execute) : base(execute)
  {
  }
}
