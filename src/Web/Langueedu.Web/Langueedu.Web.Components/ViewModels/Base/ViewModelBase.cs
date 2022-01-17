using System.ComponentModel;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;
using CurrieTechnologies.Razor.SweetAlert2;
using Langueedu.Web.Components.Exceptions;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.DependencyInjection;

namespace Langueedu.Web.Components.ViewModels;

public abstract class ViewModelBase : INotifyPropertyChanged
{
    #region Private fields

    private readonly IServiceProvider _serviceProvider;
    private SweetAlertService _swal;
    private NavigationManager _navigationManager;

    #endregion

    #region Constructor

    public ViewModelBase(IServiceProvider serviceProvider = null)
    {
        _serviceProvider = serviceProvider;
    }

    #endregion

    #region Properties

    public bool IsBusy { get; set; }

    private SweetAlertService Swal
    {
        get
        {
            if (_serviceProvider == null)
                throw new ArgumentNullException(nameof(_serviceProvider));

            if (_swal == null)
                _swal ??= _serviceProvider.GetRequiredService<SweetAlertService>();

            return _swal;
        }
    }

    private NavigationManager Navigation
    {
        get
        {
            if (_serviceProvider == null)
                throw new ArgumentNullException(nameof(_serviceProvider));

            if (_navigationManager == null)
                _navigationManager ??= _serviceProvider.GetRequiredService<NavigationManager>();

            return _navigationManager;
        }
    }

    #endregion

    #region Notify property

    private readonly Dictionary<string, List<Func<object, Task>>> _subscriptions
     = new();

    public event PropertyChangedEventHandler? PropertyChanged;

    protected bool Set<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
    {
        if (!EqualityComparer<T>.Default.Equals(field, value))
        {
            field = value;

            OnPropertyChanged(propertyName!);

            if (!_subscriptions.ContainsKey(propertyName!))
                return true;

            foreach (var action in _subscriptions[propertyName!])
                action(value!);

            return true;
        }

        return false;
    }

    public virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    protected void Subscribe<T>(Expression<Func<T>>? expression, Action<T> action)
    {
        SubscribeAsync(expression, arg =>
        {
            action(arg);
            return Task.CompletedTask;
        });
    }

    protected void SubscribeAsync<T>(Expression<Func<T>>? property, Func<T, Task> func)
    {
        if (property is null) throw new BindingException("Property cannot be null");
        if (!(property.Body is MemberExpression m))
            throw new BindingException("Subscription member must be a property");

        if (!(m.Member is PropertyInfo propertyInfo))
            throw new BindingException("Subscription member must be a property");

        var propertyName = propertyInfo.Name;
        if (!_subscriptions.ContainsKey(propertyName))
            _subscriptions[propertyName] = new List<Func<object, Task>>();

        _subscriptions[propertyName].Add(async value => await func((T)value).ConfigureAwait(false));
    }
    #endregion

    #region Lifecycle Methods

    /// <summary>
    ///     Method invoked when the component is ready to start, having received its
    ///     initial parameters from its parent in the render tree.
    /// </summary>
    public virtual void OnInitialized() { }

    /// <summary>
    ///     Method invoked when the component is ready to start, having received its
    ///     initial parameters from its parent in the render tree.
    ///     Override this method if you will perform an asynchronous operation and
    ///     want the component to refresh when that operation is completed.
    /// </summary>
    /// <returns>A <see cref="Task" /> representing any asynchronous operation.</returns>
    public virtual Task OnInitializedAsync()
    {
        return Task.CompletedTask;
    }

    public virtual void OnParametersSet() { }
    
    public virtual Task SetParametersAsync(ParameterView parameters)
    {
        return Task.CompletedTask;
    }

    #endregion

    #region Dialogs

    protected async Task ShowErrorAsync(params string[] errorMessages)
    {
        if (errorMessages == null || !errorMessages.Any())
            return;

        foreach (var errorMessage in errorMessages)
        {
            await Swal.FireAsync(errorMessage, icon: SweetAlertIcon.Error);
        }
    }

    protected Task ShowErrorAsync(IEnumerable<string> errorMessages)
    {
        return ShowErrorAsync(errorMessages.ToArray());
    }

    #endregion

    #region Navigation

    protected void NavigateTo(string uri, bool forceLoad = false)
    {
        Navigation.NavigateTo(uri, forceLoad);
    }

    #endregion
}