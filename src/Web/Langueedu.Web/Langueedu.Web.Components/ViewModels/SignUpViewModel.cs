using System.Windows.Input;
using Langueedu.Sdk.Identity.Response;
using Langueedu.Web.Shared.Utilities;

namespace Langueedu.Web.Components.ViewModels;

public class SignUpViewModel : ViewModelBase
{
    private SignUpModel _signUpModel = new();
    private ICommand _loginCommand;

    public SignUpModel SignUpModel
    {
        get => _signUpModel;
        set => Set(ref _signUpModel, value);
    }

    private void SignUpCommandExecute()
    {
        Console.WriteLine("--- LoginCommandExecute ---");
        Console.WriteLine(SignUpModel.UserName);
        Console.WriteLine(SignUpModel.Password);
    }

    public ICommand SignUpCommand { get { return _loginCommand = (_loginCommand ?? new Command(SignUpCommandExecute)); } }
}