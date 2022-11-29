using System.Windows.Input;
using Langueedu.Components.Interfaces;
using Langueedu.Sdk.Account;
using Langueedu.Sdk.Account.Request;
using Langueedu.Sdk.Account.Response;
using Langueedu.Web.Shared.Utilities;

namespace Langueedu.Components.ViewModels;

public class SignUpViewModel : ViewModelBase
{
  private SignUpModel _signUpModel = new();
  private ICommand _signUpCommand;
  private readonly IAccountService _accountService;
  private readonly SignInViewModel _signInViewModel;
  private readonly ICultureService _cultureService;

  public SignUpViewModel(IAccountService accountService,
                         SignInViewModel signInViewModel,
                         IServiceProvider serviceProvider,
                         ICultureService cultureService) : base(serviceProvider)
  {
    _accountService = accountService;
    _signInViewModel = signInViewModel;
    _cultureService = cultureService;
  }

  public SignUpModel SignUpModel
  {
    get => _signUpModel;
    set => SetField(ref _signUpModel, value);
  }

  private async Task SignUpCommandExecute()
  {
    if (IsBusy)
      return;

    IsBusy = true;

    string culture = await _cultureService.GetCulture();

    SignUpModel.LanguageCode = culture;

    var response = await _accountService.SignUpAsync(SignUpModel);
    if (!response.IsSuccess)
    {
      await ShowErrorAsync(response.Errors);

      IsBusy = false;

      return;
    }

    _signInViewModel.LoginModel = new LoginModel
    {
      UserName = SignUpModel.UserName,
      Password = SignUpModel.Password
    };

    _signInViewModel.LoginCommand.Execute(null);

    IsBusy = false;
  }

  public ICommand SignUpCommand { get => _signUpCommand ??= new Command(SignUpCommandExecute); }
}
