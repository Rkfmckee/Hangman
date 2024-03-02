using Hangman.API.Models;
using Hangman.API.ViewModels.Users;
using Hangman.Data.Interfaces;
using Microsoft.AspNetCore.Mvc;
using BC = BCrypt.Net.BCrypt;

namespace Hangman.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IRepository<User> userRepository;

        public UserController(IRepository<User> userRepository)
        {
            this.userRepository = userRepository;
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
            return null;
        }
    }
}
