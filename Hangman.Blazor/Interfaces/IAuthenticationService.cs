using Hangman.Blazor.ViewModels;

namespace Hangman.Blazor.Interfaces
{
    public interface IAuthenticationService
    {
        public ValueTask<string> GetJwtAsync();
        public Task LoginAsync(LoginViewModel loginViewModel);
        public Task LogoutAsync();
    }
}
