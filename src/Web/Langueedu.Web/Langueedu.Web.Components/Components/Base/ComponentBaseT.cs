using Langueedu.Web.Components.ViewModels;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.DependencyInjection;

namespace Langueedu.Web.Components;

public abstract class ComponentBase<T> : ComponentBase where T : ViewModelBase
{
    private IServiceProvider _serviceProvider;

    protected internal T BindingContext { get; set; } = null!;

    [Inject]
    public IServiceProvider ServiceProvider
    {
        get => _serviceProvider;
        set => _serviceProvider = value;
    }

    protected override void OnInitialized()
    {
        base.OnInitialized();
        SetBindingContext();
        BindingContext?.OnInitialized();
    }

    private void SetBindingContext()
    {
        BindingContext ??= _serviceProvider.GetRequiredService<T>();
    }

    protected override Task OnInitializedAsync()
    {
        return BindingContext?.OnInitializedAsync() ?? Task.CompletedTask;
    }
}
