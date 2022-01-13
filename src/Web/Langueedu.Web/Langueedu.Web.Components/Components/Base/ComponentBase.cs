namespace Langueedu.Web.Components;

public abstract class ComponentBase : Microsoft.AspNetCore.Components.ComponentBase, IDisposable, IAsyncDisposable
{

    #region IDisposable Support

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {

    }

    public async ValueTask DisposeAsync()
    {
        await DisposeAsyncCore();

        Dispose(false);
        GC.SuppressFinalize(this);
    }

    protected virtual ValueTask DisposeAsyncCore()
    {
        return default;
    }


    ~ComponentBase()
    {
        Dispose(false);
    }

    #endregion
}