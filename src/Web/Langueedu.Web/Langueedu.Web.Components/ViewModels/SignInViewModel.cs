using System.Net.Http.Headers;
using System.Windows.Input;
using Blazored.LocalStorage;
using Langueedu.Sdk.Identity;
using Langueedu.Sdk.Identity.Request;
using Langueedu.Web.Components.Provider;
using Langueedu.Web.Shared.Utilities;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System.Text.Json;
using System.Text.Json.Serialization;
using Langueedu.Sdk.Identity.Response;
using CurrieTechnologies.Razor.SweetAlert2;

namespace Langueedu.Web.Components.ViewModels;

public class SignInViewModel : ViewModelBase
{
    private LoginModel _loginModel = new() { UserName = "witcherfearless", Password = "12345678" };
    private ICommand _loginCommand;
    private readonly IIdentityService _identityService;
    private readonly ILocalStorageService _localStorageService;
    private readonly HttpClient _httpClient;
    private readonly AuthenticationStateProvider _authenticationStateProvider;

    public SignInViewModel(IIdentityService identityService,
                           ILocalStorageService localStorageService,
                           HttpClient httpClient,
                           IServiceProvider serviceProvider,
                           AuthenticationStateProvider authenticationStateProvider) : base(serviceProvider)
    {
        _identityService = identityService;
        _localStorageService = localStorageService;
        _httpClient = httpClient;
        _authenticationStateProvider = authenticationStateProvider;
    }



    public LoginModel LoginModel
    {
        get => _loginModel;
        set => Set(ref _loginModel, value);
    }

    private async Task LoginCommandExecute()
    {
        var response = await _identityService.SignInAsync(LoginModel);
        if (!response.IsSuccess)
        {
            await ShowErrorAsync(response.Errors);

            return;
        }

        await (_authenticationStateProvider as AuthStateProvider).NotifyUserLogin(response.Value);

        NavigateTo("/Counter");
    }

    public ICommand LoginCommand { get => _loginCommand ??= new CommandAsync(LoginCommandExecute); }
}