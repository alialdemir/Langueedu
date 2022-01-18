using Blazored.Modal;
using Langueedu.Web.Components.PropertyBinding;
using Langueedu.Web.Components.ViewModels;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.DependencyInjection;

namespace Langueedu.Web.Components;

public abstract class ComponentBase<T> : ComponentBase where T : ViewModelBase
{
    private IServiceProvider _serviceProvider;

    private IViewModelParameterSetter? _viewModelParameterSetter;
    protected internal T BindingContext { get; set; } = null!;

    [Inject]
    public IServiceProvider ServiceProvider
    {
        get => _serviceProvider;
        set => _serviceProvider = value;
    }

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
        BindingContext ??= _serviceProvider.GetRequiredService<T>();
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
}
