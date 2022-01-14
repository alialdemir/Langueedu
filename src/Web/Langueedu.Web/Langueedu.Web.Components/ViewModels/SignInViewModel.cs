using System.Windows.Input;
using Langueedu.Sdk.Identity;
using Langueedu.Sdk.Identity.Request;
using Langueedu.Web.Shared.Utilities;
using Microsoft.AspNetCore.Components;

namespace Langueedu.Web.Components.ViewModels;

public class SignInViewModel : ViewModelBase
{
    private LoginModel _loginModel = new() { UserName = "witcherfearless", Password = "12345678" };
    private ICommand _loginCommand;
    private readonly IIdentityService _identityService;

    public SignInViewModel(IIdentityService identityService)
    {
        _identityService = identityService;
    }



    public LoginModel LoginModel
    {
        get => _loginModel;
        set => Set(ref _loginModel, value);
    }

    private async Task LoginCommandExecute()
    {
        var response = await _identityService.SignInAsync(LoginModel);
        Console.WriteLine("çalıştı *-*-- {0}", response.IsSuccess);
        if (response.IsSuccess)
        {
            // redirect
        }

    }

    public ICommand LoginCommand { get { return _loginCommand = (_loginCommand ?? new CommandAsync(LoginCommandExecute)); } }
}