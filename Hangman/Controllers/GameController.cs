using Hangman.Data.Interfaces;
using Hangman.Models;
using Microsoft.AspNetCore.Mvc;

namespace Hangman.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GameController : ControllerBase
    {
        #region Fields

        private readonly IRepository<Game> gameRepo;
        private readonly IRepository<Guess> guessRepo;

        #endregion

        #region Constructors

        public GameController(IRepository<Game> gameRepo, IRepository<Guess> guessRepo)
        {
            this.gameRepo = gameRepo;
            this.guessRepo = guessRepo;
        }

        #endregion

        #region Actions

        [HttpGet]
        public Game GetGame(int id)
        {
            return new Game();
        }

        [HttpPost]
        public Game CreateGame()
        {
            return new Game();
        }

        #endregion
    }
}