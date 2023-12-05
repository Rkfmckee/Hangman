using Hangman.Data;
using Hangman.Data.Interfaces;
using Hangman.Enums;
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
        public IActionResult GetGame(Guid id)
        {
            var game = gameRepo.Get(id);
            if (game == null) return NotFound();

            var result = new
            {
                gameStatus = game.GameStatus.GetDescription(),
                word = AddSpacesBetweenLetters(game.CorrectLetters)
            };

            return Ok(result);
        }

        [HttpPost]
        [Route("Game")]
        public IActionResult CreateGame()
        {
            var skipNum = random.Next(0, dbContext.Words.Count());
            var word    = dbContext.Words.OrderBy(w => w.Id).Skip(skipNum).First().Word;

            var game = new Game(word);
            gameRepo.Add(game);

            var result = new
            {
                gameId = game.Id
            };

            return Ok(result);
        }

        [HttpPost]
        [Route("Game/{id}/Guess/{guessChar}")]
        public IActionResult Guess(Guid id, char guessChar)
        {
            var game = gameRepo.Get(id);
            if (game == null) return NotFound();

            if (game.GameStatus != GameStatus.InProgress) return BadRequest(new { error = $"You can only guess in a game which is in progress." });

            guessChar = char.ToLower(guessChar);

            var alreadyGuessedLetter = game.Guesses.Select(g => g.CharacterGuessed).Contains(guessChar);
            if (alreadyGuessedLetter) return BadRequest(new { error = $"You already guessed the letter {guessChar}." });

            var guessCorrect = game.Word.Contains(guessChar);
            var guess        = new Guess(guessChar, guessCorrect, id);

            if (guessCorrect)
            {
                var positionsOfLetter = CorrectLetterPositions(game.Word, guessChar);

                foreach (var position in positionsOfLetter)
                {
                    game.CorrectLetters = game.CorrectLetters.ReplaceCharAt(position, guessChar);
                }
            }
            else
            {
                game.IncorrectGuessesLeft--;

                if (game.IncorrectGuessesLeft <= 0)
                {
                    game.GameStatus = GameStatus.Lost;
                }
            }

            gameRepo.Update(game);
            guessRepo.Add(guess);

            var result = new
            {
                guessCorrect              = guessCorrect,
                word                      = AddSpacesBetweenLetters(game.CorrectLetters),
                incorrectGuessesRemaining = game.IncorrectGuessesLeft
            };

            return Ok(result);
        }

        #endregion

        #region Methods

        private string AddSpacesBetweenLetters(string word)
        {
            return string.Join("", word.Select(c => c + " ")).Trim();
        }

        private List<int> CorrectLetterPositions(string word, char guess)
        {
            var positions = new List<int>();

            while (true)
            {
                var position = word.IndexOf(guess);
                if (position == -1) break;

                word = word.ReplaceCharAt(position, '-');
                positions.Add(position);
            }

            return positions;
        }

        #endregion
    }
}