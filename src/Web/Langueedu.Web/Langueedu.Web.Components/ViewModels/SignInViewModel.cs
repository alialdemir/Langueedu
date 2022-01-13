using Langueedu.Web.Components.Models;

namespace Langueedu.Web.Components.ViewModels;

public class SignInViewModel : ViewModelBase
{

    private LoginModel _loginModel = new();

    public LoginModel LoginModel
    {
        get => _loginModel;
        set => Set(ref _loginModel, value);
    }
}