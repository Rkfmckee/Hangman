using Hangman.Data;
using Hangman.Data.Interfaces;
using Hangman.Helpers;
using Hangman.Models;
using Microsoft.AspNetCore.Mvc;

namespace Hangman.Controllers
{
    [ApiController]
    public class GameController : ControllerBase
    {
        #region Fields

        private readonly ApplicationDbContext dbContext;
        private readonly IRepository<Game> gameRepo;
        private readonly IRepository<Guess> guessRepo;
        private readonly IRepository<Words> wordRepo;

        private Random random;

        #endregion

        #region Constructors

        public GameController(ApplicationDbContext dbContext, IRepository<Game> gameRepo, IRepository<Guess> guessRepo, IRepository<Words> wordRepo)
        {
            this.dbContext = dbContext;
            this.gameRepo  = gameRepo;
            this.guessRepo = guessRepo;
            this.wordRepo  = wordRepo;

            random = new Random();
        }

        #endregion

        #region Actions

        [HttpGet]
        [Route("Game/{id}")]
        public IActionResult GetGame(int id)
        {
            var game = gameRepo.Get(id);
            if (game == null) return NotFound();

            var correctLettersWithSpaces = AddSpacesBetweenLetters(game.CorrectLetters);

            var result = new
            {
                gameStatus = game.GameStatus.GetDescription(),
                correctLetters = correctLettersWithSpaces
            };

            return Ok(result);
        }

        [HttpPost]
        [Route("Game")]
        public IActionResult CreateGame()
        {
            var totalWords = dbContext.Words.Count();
            var wordId     = random.Next(0, totalWords);
            var word       = wordRepo.Get(wordId).Word;

            var game = new Game(word);
            gameRepo.Add(game);

            var result = new
            {
                gameId = game.Id
            };

            return Ok(result);
        }

        //[HttpPost]
        //public IActionResult Guess(int id, char guess)
        //{


        //    return Ok();
        //}

        #endregion

        #region Methods

        private string AddSpacesBetweenLetters(string word)
        {
            return string.Join("", word.Select(c => c + " ")).Trim();
        }

        #endregion
    }
}