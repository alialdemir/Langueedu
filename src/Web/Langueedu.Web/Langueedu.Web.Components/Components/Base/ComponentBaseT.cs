using System.Linq.Expressions;
using Langueedu.Web.Components.Internal.PropertyBinding;
using Langueedu.Web.Components.ViewModels;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.DependencyInjection;

namespace Langueedu.Web.Components;

public abstract class ComponentBase<T> : ComponentBase where T : ViewModelBase
{

    private IViewModelParameterSetter _viewModelParameterSetter;
    protected internal T BindingContext { get; set; } = null!;


    private void SetParameters()
    {
        if (BindingContext is null)
            throw new InvalidOperationException($"{nameof(BindingContext)} is not set");

        _viewModelParameterSetter ??= ServiceProvider.GetRequiredService<IViewModelParameterSetter>();
        _viewModelParameterSetter.ResolveAndSet(this, BindingContext);
    }

    protected override void OnInitialized()
    {
        base.OnInitialized();
        SetBindingContext();
        SetParameters();
        BindingContext?.OnInitialized();
    }

    private void SetBindingContext()
    {
        BindingContext ??= ServiceProvider.GetRequiredService<T>();
    }

    protected override Task OnInitializedAsync()
    {
        return BindingContext?.OnInitializedAsync() ?? Task.CompletedTask;
    }

    protected override Task OnAfterRenderAsync(bool firstRender)
    {
        return BindingContext?.OnAfterRenderAsync(firstRender) ?? Task.CompletedTask;
    }

    protected override void OnParametersSet()
    {
        SetParameters();
        BindingContext?.OnParametersSet();
    }

    public override async Task SetParametersAsync(ParameterView parameters)
    {
        await base.SetParametersAsync(parameters).ConfigureAwait(false);

        if (BindingContext != null)
            await BindingContext.SetParametersAsync(parameters).ConfigureAwait(false);
    }
    protected internal TValue Bind<TValue>(Expression<Func<T, TValue>> property)
    {
        if (BindingContext is null)
            throw new InvalidOperationException($"{nameof(BindingContext)} is not set");

        return AddBinding(BindingContext, property);
    }

}
