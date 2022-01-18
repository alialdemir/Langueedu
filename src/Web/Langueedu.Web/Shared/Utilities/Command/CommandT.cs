using System.Reflection;

namespace Langueedu.Web.Shared.Utilities;

public sealed class Command<T> : Command
{
    public Command(Action<T> execute)
        : base(o =>
        {
            if (IsValidParameter(o))
            {
                execute((T)o);
            }
        })
    {
        if (execute == null)
        {
            throw new ArgumentNullException(nameof(execute));
        }
    }

    public Command(Action<T> execute, Func<T, bool> canExecute)
        : base(o =>
        {
            if (IsValidParameter(o))
            {
                execute((T)o);
            }
        }, o => IsValidParameter(o) && canExecute((T)o))
    {
        if (execute == null)
            throw new ArgumentNullException(nameof(execute));
        if (canExecute == null)
            throw new ArgumentNullException(nameof(canExecute));
    }
    
    static bool IsValidParameter(object o)
    {
        if (o != null)
        {
            // The parameter isn't null, so we don't have to worry whether null is a valid option
            return o is T;
        }

        var t = typeof(T);

        // The parameter is null. Is T Nullable?
        if (Nullable.GetUnderlyingType(t) != null)
        {
            return true;
        }

        // Not a Nullable, if it's a value type then null is not valid
        return !t.GetTypeInfo().IsValueType;
    }
}
