using Hangman.Blazor.Interfaces;
using System.Net.Http.Headers;

namespace Hangman.Blazor.Authentication
{
    public class AuthenticationHandler : DelegatingHandler
    {
        private readonly IAuthenticationService authenticationService;
        private readonly IConfiguration configuration;

        public AuthenticationHandler(IAuthenticationService authenticationService, IConfiguration configuration)
        {
            this.authenticationService = authenticationService;
            this.configuration = configuration;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            // Intercept all http requests and add the jwt to it's authentication header
            var jwt = await authenticationService.GetJwtAsync();
            var toCorrectServer = request.RequestUri?.AbsoluteUri.StartsWith(configuration["ApiUri"] ?? "") ?? false;

            if (toCorrectServer && !string.IsNullOrEmpty(jwt))
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", jwt);

            return await base.SendAsync(request, cancellationToken);
        }
    }
}
