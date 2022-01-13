using Langueedu.Web.Components.ViewModels;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.Extensions.DependencyInjection;

namespace Langueedu.Web.Components;

public class ComponentBase<TViewModel> : Microsoft.AspNetCore.Components.ComponentBase where TViewModel : ViewModelBase
{

    protected ComponentBase() { }

    protected internal ComponentBase(IServiceProvider serviceProvider)
    {
        ServiceProvider = serviceProvider;
        SetBindingContext();
    }


    internal TViewModel ViewModel { get; private set; }

    [Inject] protected IServiceProvider ServiceProvider { get; set; } = null!;

    private void SetBindingContext()
    {
        // ReSharper disable once ConstantNullCoalescingCondition
        ViewModel ??= ServiceProvider.GetRequiredService<TViewModel>();
    }

    protected override void OnInitialized()
    {
        base.OnInitialized();
        ViewModel ??= ServiceProvider.GetRequiredService<TViewModel>();
    }
}