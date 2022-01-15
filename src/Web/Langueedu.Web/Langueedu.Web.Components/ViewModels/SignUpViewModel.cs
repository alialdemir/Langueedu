using System.Windows.Input;
using Langueedu.Sdk.Identity;
using Langueedu.Sdk.Identity.Response;
using Langueedu.Web.Shared.Utilities;

namespace Langueedu.Web.Components.ViewModels;

public class SignUpViewModel : ViewModelBase
{
    private SignUpModel _signUpModel = new();
    private ICommand _loginCommand;
    private readonly IIdentityService _identityService;

    public SignUpViewModel(IIdentityService identityService,
                           IServiceProvider serviceProvider) : base(serviceProvider)
    {
        _identityService = identityService;
    }

    public SignUpModel SignUpModel
    {
        get => _signUpModel;
        set => Set(ref _signUpModel, value);
    }

    private async Task SignUpCommandExecute()
    {
        var response = await _identityService.SignUpAsync(SignUpModel);
        if (!response.IsSuccess)
        {
            await ShowErrorAsync(response.Errors);

            return;
        }
    }

    public ICommand SignUpCommand { get { return _loginCommand = (_loginCommand ?? new CommandAsync(SignUpCommandExecute)); } }
}