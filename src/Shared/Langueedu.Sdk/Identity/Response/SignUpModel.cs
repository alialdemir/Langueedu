namespace Langueedu.Sdk.Identity.Response
{
    public class SignUpModel
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string LanguageCode { get; set; }
    }
}