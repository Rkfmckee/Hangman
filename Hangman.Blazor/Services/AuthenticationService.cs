using Blazored.SessionStorage;
using Hangman.Blazor.Interfaces;
using Hangman.Blazor.ViewModels;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Text.Json;
using System.Text;

namespace Hangman.Blazor.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private const string JWT_KEY = nameof(JWT_KEY);

        private readonly IHttpClientFactory factory;
        private readonly ISessionStorageService sessionStorageService;

        public AuthenticationService(IHttpClientFactory factory, ISessionStorageService sessionStorageService)
        {
            this.factory = factory;
            this.sessionStorageService = sessionStorageService;
        }

        public event Action<string?>? LoginChange;

        public async ValueTask<string> GetJwtAsync()
        {
            return await sessionStorageService.GetItemAsync<string>(JWT_KEY);
        }

        public async Task LoginAsync(LoginViewModel loginViewModel)
        {
            var request = new StringContent(JsonSerializer.Serialize(loginViewModel), Encoding.UTF8, "application/json");
            var response = await factory.CreateClient("ServerApi").PostAsync("api/User/login", request);
            if (!response.IsSuccessStatusCode) throw new UnauthorizedAccessException("Login failed");

            var content = await response.Content.ReadFromJsonAsync<AuthTokenViewModel>();
            if (content == null) throw new InvalidDataException();

            await sessionStorageService.SetItemAsync(JWT_KEY, content.AuthToken);

            LoginChange?.Invoke(GetUsername(content.AuthToken));
        }

        public async Task LogoutAsync()
        {
            await sessionStorageService.RemoveItemAsync(JWT_KEY);

            LoginChange?.Invoke(null);
        }

        private static string GetUsername(string token)
        {
            var jwt = new JwtSecurityToken(token);
            return jwt.Claims.First(c => c.Type == ClaimTypes.Name).Value;
        }
    }
}
