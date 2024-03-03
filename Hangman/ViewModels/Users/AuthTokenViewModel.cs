namespace Hangman.API.ViewModels.Users
{
    public class AuthTokenViewModel
    {
        public AuthTokenViewModel(string authToken)
        {
            AuthToken = authToken;
        }

        public string AuthToken { get; set; }
    }
}
