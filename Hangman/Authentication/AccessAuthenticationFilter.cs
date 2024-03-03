using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Hangman.API.Authentication;

public class AccessAuthenticationFilter : AuthenticationHandler<AuthenticationSchemeOptions>
{
    private readonly IHttpContextAccessor _contextAccessor;
    private readonly IConfiguration _configuration;

    public AccessAuthenticationFilter(IOptionsMonitor<AuthenticationSchemeOptions> options, ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock, IHttpContextAccessor accessor, IConfiguration configuration)
        : base(options, logger, encoder, clock)
    {
        _contextAccessor = accessor;
        _configuration = configuration;
    }

    protected override Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        var header = _contextAccessor.HttpContext?.Request.Headers["authorization"].ToString().Replace("Bearer ", string.Empty);
        var handler = new JwtSecurityTokenHandler();
        var secretKey = Encoding.UTF8.GetBytes(_configuration.GetValue<string>("Keys:JwtKey"));

        var validation = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(secretKey),
            ValidateAudience = false,
            ValidateIssuer = false
        };

        try
        {
            var principal = handler.ValidateToken(header, validation, out var validatedToken);

            var ticket = new AuthenticationTicket(principal, string.Empty);

            return Task.FromResult(AuthenticateResult.Success(ticket));
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return Task.FromResult(AuthenticateResult.Fail("Authentication Failed"));

        }
    }
}