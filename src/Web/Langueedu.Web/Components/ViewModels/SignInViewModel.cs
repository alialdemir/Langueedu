using System.Windows.Input;
using Blazored.LocalStorage;
using Langueedu.Components.Provider;
using Langueedu.Sdk.Account;
using Langueedu.Sdk.Account.Request;
using Langueedu.Web.Shared.Utilities;
using Microsoft.AspNetCore.Components.Authorization;

namespace Langueedu.Components.ViewModels;

public class SignInViewModel : ViewModelBase
{
  private LoginModel _loginModel = new() { UserName = "witcherfearless", Password = "12345678" };
  private ICommand _loginCommand;
  private readonly IAccountService _accountService;
  private readonly ILocalStorageService _localStorageService;
  private readonly HttpClient _httpClient;
  private readonly AuthenticationStateProvider _authenticationStateProvider;

  public SignInViewModel(IAccountService accountService,
                         ILocalStorageService localStorageService,
                         HttpClient httpClient,
                         IServiceProvider serviceProvider,
                         AuthenticationStateProvider authenticationStateProvider) : base(serviceProvider)
  {
    _accountService = accountService;
    _localStorageService = localStorageService;
    _httpClient = httpClient;
    _authenticationStateProvider = authenticationStateProvider;
  }

  public LoginModel LoginModel
  {
    get => _loginModel;
    set => SetField(ref _loginModel, value);
  }

  private async Task LoginCommandExecute()
  {
    var response = await _accountService.SignInAsync(LoginModel);
    if (!response.IsSuccess)
    {
      await ShowErrorAsync(response.Errors);

      return;
    }

    System.Console.WriteLine($"token: {response.Value.AccessToken}");

    await (_authenticationStateProvider as AuthStateProvider).NotifyUserLogin(response.Value);

    NavigateTo("/Learn", true);
  }

  public ICommand LoginCommand { get => _loginCommand ??= new Command(LoginCommandExecute); }
}
