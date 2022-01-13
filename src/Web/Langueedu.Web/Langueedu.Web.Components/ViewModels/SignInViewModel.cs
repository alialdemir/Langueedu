using Langueedu.Web.Components.Models;

namespace Langueedu.Web.Components.ViewModels;

public class SignInViewModel : ViewModelBase
{

    public LoginModel LoginModel { get; set; } = new() { UserName = "test" };


}