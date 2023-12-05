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

        private readonly IRepository<Game> gameRepo;
        private readonly IRepository<Guess> guessRepo;
        private readonly IRepository<Words> wordRepo;

        private Random random;

        #endregion

        #region Constructors

        public GameController(IRepository<Game> gameRepo, IRepository<Guess> guessRepo, IRepository<Words> wordRepo)
        {
            this.gameRepo  = gameRepo;
            this.guessRepo = guessRepo;
            this.wordRepo  = wordRepo;

            random = new Random();
        }

        #endregion

        #region Actions

        [HttpPost]
        [Route("Game")]
        public IActionResult CreateGame()
        {
            var words   = wordRepo.GetAll();
            var skipNum = random.Next(0, words.Count());
            var word    = words.OrderBy(w => w.Id).Skip(skipNum).First();

            var game = new Game(word);
            gameRepo.Add(game);

            var result = new
            {
                gameId = game.Id
            };

            return Ok(result);
        }

        [HttpGet]
        [Route("Game/{id}")]
        public IActionResult GetGame(Guid id)
        {
            var game = gameRepo.Get(id);
            if (game == null) return NotFound();

            var result = new
            {
                gameStatus = game.GameStatus.GetDescription(),
                word = game.CorrectLetters.AddSpacesBetweenLetters(),
                incorrectGuessesRemaining = game.IncorrectGuessesLeft,
                guesses = GetCharsOfGuesses(game.Guesses)
            };

            return Ok(result);
        }

        [HttpGet]
        [Route("Games")]
        public IActionResult GetAllGames()
        {
            var games = gameRepo.GetAll();
            if (games == null) return NotFound();

            var gameStates = new Dictionary<Guid, string>();

            foreach (var game in games)
            {
                gameStates.Add(game.Id, game.GameStatus.GetDescription());
            }

            return Ok(gameStates);
        }

        [HttpPost]
        [Route("Game/{id}/Guess/{guessChar}")]
        public IActionResult Guess(Guid id, char guessChar)
        {
            var game = gameRepo.Get(id);
            if (game == null) return NotFound();

            if (game.GameStatus != GameStatus.InProgress) return BadRequest(new { error = $"You can only guess in a game which is in progress." });

            if (!char.IsLetter(guessChar)) return BadRequest(new { error = $"You can only guess letters." });

            guessChar = char.ToUpper(guessChar);

            var alreadyGuessedLetter = game.Guesses.Select(g => g.CharacterGuessed).Contains(guessChar);
            if (alreadyGuessedLetter) return BadRequest(new { error = $"You already guessed the letter {guessChar}." });

            var guessCorrect = game.ChosenWord.Word.Contains(guessChar);
            var guess        = new Guess(guessChar, guessCorrect, id);

            if (guessCorrect)
            {
                var positionsOfLetter = CorrectLetterPositions(game.ChosenWord.Word, guessChar);

                foreach (var position in positionsOfLetter)
                {
                    game.CorrectLetters = game.CorrectLetters.ReplaceCharAt(position, guessChar);
                }

                if (string.Equals(game.ChosenWord.Word, game.CorrectLetters)) game.GameStatus = GameStatus.Won;
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
                word                      = game.CorrectLetters.AddSpacesBetweenLetters(),
                incorrectGuessesRemaining = game.IncorrectGuessesLeft,
                guesses                   = GetCharsOfGuesses(game.Guesses)
            };

            return Ok(result);
        }

        [HttpDelete]
        [Route("Game/{id}")]
        public IActionResult DeleteGame(Guid id)
        {
            var game = gameRepo.Get(id);
            if (game == null) return NotFound();

            gameRepo.Delete(game);

            return NoContent();
        }

        #endregion

        #region Methods

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

        private string GetCharsOfGuesses(List<Guess> guesses)
        {
            string characters = string.Empty;

            foreach (var guess in guesses)
            {
                characters += $"{guess.CharacterGuessed}, ";
            }

            characters = characters.Trim().Trim(',');
            return characters;
        }

        #endregion
    }
}