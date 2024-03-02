using Hangman.API.Data;
using Hangman.API.Models;
using Hangman.API.ViewModels.Users;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Unicode;
using BC = BCrypt.Net.BCrypt;

namespace Hangman.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository userRepository;
        private readonly IConfiguration configuration;

        public UserController(IUserRepository userRepository, IConfiguration configuration)
        {
            this.userRepository = userRepository;
            this.configuration = configuration;
        }

        [HttpPost("register")]
        public ActionResult Register(RegisterViewModel registerDetails)
        {
            var user = new User(registerDetails.Username, BC.HashPassword(registerDetails.Password));
            
            userRepository.Add(user);

            return NoContent();
        }

        [HttpPost("login")]
        public ActionResult<User> Login(LoginViewModel loginDetails)
        {
            var user = userRepository.Get(loginDetails.Username);
            if (user == null) return NotFound();

            var passwordCorrect = BC.Verify(loginDetails.Password, user.Password);
            if (!passwordCorrect) return NotFound();

            var tokenViewModel = new AuthTokenViewModel(CreateToken(user));

            return Ok(tokenViewModel);
        }

        private string CreateToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username)
            };

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.GetSection("Keys:JwtKey").Value));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512Signature);
            var token = new JwtSecurityToken(claims: claims, expires: DateTime.UtcNow.AddDays(1), signingCredentials: credentials);
            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }
    }
}
