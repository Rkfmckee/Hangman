using Hangman.Blazor.Interfaces;
using Microsoft.AspNetCore.Components.Authorization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Hangman.Blazor.Authentication
{
    public class CustomAuthStateProvider : AuthenticationStateProvider
    {
        private readonly IAuthenticationService authenticationService;

        public CustomAuthStateProvider(IAuthenticationService authenticationService)
        {
            this.authenticationService = authenticationService;
        }

        // Handling the Authorization for the blazor pages
        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var jwtToken = await authenticationService.GetJwtAsync();
            var identity = new ClaimsIdentity();

            if (jwtToken != null)
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var token = tokenHandler.ReadJwtToken(jwtToken);
                identity = new ClaimsIdentity(token.Claims, "jwt");
            }

            var user = new ClaimsPrincipal(identity);
            var state = new AuthenticationState(user);

            NotifyAuthenticationStateChanged(Task.FromResult(state));

            return state;
        }
    }
}
