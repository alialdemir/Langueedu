using System.Windows.Input;
using Langueedu.Web.Components.Models;
using Langueedu.Web.Shared.Utilities;
using Microsoft.AspNetCore.Components.Forms;

namespace Langueedu.Web.Components.ViewModels;

public class SignInViewModel : ViewModelBase
{
    public SignInViewModel()
    {

    }
    private LoginModel _loginModel = new();
    private ICommand _loginCommand;

    public LoginModel LoginModel
    {
        get => _loginModel;
        set => Set(ref _loginModel, value);
    }

    private void LoginCommandExecute()
    {
        Console.WriteLine("--- LoginCommandExecute --- çalıştı");
        Console.WriteLine(LoginModel.UserName);
        Console.WriteLine(LoginModel.Password);
        Console.WriteLine("--- LoginCommandExecute --- çalıştı");
    }

    public ICommand LoginCommand { get { return _loginCommand = (_loginCommand ?? new Command(LoginCommandExecute)); } }

}