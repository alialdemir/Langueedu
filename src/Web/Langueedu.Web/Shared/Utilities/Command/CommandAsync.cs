namespace Langueedu.Web.Shared.Utilities;

public partial class CommandAsync : Command
{
    public CommandAsync(Func<Task> execute) : base(() => execute())
    {
    }

    public CommandAsync(Func<object, Task> execute) : base(() => execute(null))
    {
    }
}