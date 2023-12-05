using FakeItEasy;
using Hangman.Controllers;
using Hangman.Data.Interfaces;
using Hangman.Models;
using Hangman.Helpers;
using Hangman.Enums;
using Microsoft.AspNetCore.Mvc;

namespace Hangman.Tests.Controllers
{
    public class GameControllerTests
    {
        #region Fields

        private IRepository<Game> gameRepo;
        private IRepository<Guess> guessRepo;
        private IRepository<Words> wordRepo;

        private GameController controller;

        #endregion

        #region Tests

        [SetUp]
        public void Setup()
        {
            gameRepo  = A.Fake<IRepository<Game>>();
            guessRepo = A.Fake<IRepository<Guess>>();
            wordRepo  = A.Fake<IRepository<Words>>();

            controller = new GameController(gameRepo, guessRepo, wordRepo);
        }

        [Test]
        public void CreateGame_Successfully_Creates_A_Game()
        {
            A.CallTo(() => wordRepo.GetAll()).Returns(WordsList());

            var actionResult = controller.CreateGame() as OkObjectResult;
            var gameId       = GetPropertyOfAnonymousObject(actionResult?.Value, "gameId");

            Assert.NotNull(gameId);
            Assert.IsInstanceOf<Guid>(gameId);
        }

        [Test]
        public void GetGame_Returns_NotFound_When_Game_Doesnt_Exist()
        {
            A.CallTo(() => gameRepo.Get(1.ToGuid())).Returns(null);

            var actionResult = controller.GetGame(1.ToGuid()) as NotFoundResult;

            Assert.That(actionResult?.StatusCode, Is.EqualTo(404));
        }

        [Test]
        public void GetGame_Returns_The_Correct_Game()
        {
            var game = SingleGame();

            A.CallTo(() => gameRepo.Get(game.Id)).Returns(game);

            var actionResult = controller.GetGame(game.Id) as OkObjectResult;
            var gameStatus   = GetPropertyOfAnonymousObject(actionResult?.Value, "gameStatus") as string;
            var word         = GetPropertyOfAnonymousObject(actionResult?.Value, "word") as string;

            Assert.That(actionResult?.StatusCode, Is.EqualTo(200));
            Assert.That(gameStatus, Is.EqualTo("In progress"));
            Assert.That(game.CorrectLetters.AddSpacesBetweenLetters(), Is.EqualTo(word));
        }

        [Test]
        public void GetAllGames_Returns_NotFound_When_No_Games_Have_Been_Created()
        {
            A.CallTo(() => gameRepo.GetAll()).Returns(null);

            var actionResult = controller.GetAllGames() as NotFoundResult;

            Assert.That(actionResult?.StatusCode, Is.EqualTo(404));
        }

        [Test]
        public void GetAllGames_Returns_Correct_List_Of_Games()
        {
            A.CallTo(() => gameRepo.GetAll()).Returns(GamesList());

            var actionResult = controller.GetAllGames() as OkObjectResult;
            var dictionary   = actionResult?.Value as Dictionary<Guid, string>;

            Assert.That(dictionary.Keys.Count, Is.EqualTo(3));
            Assert.That(dictionary, Contains.Key(1.ToGuid()));
            Assert.That(dictionary, Contains.Key(2.ToGuid()));
            Assert.That(dictionary, Contains.Key(3.ToGuid()));
        }

        [Test]
        public void Guess_Returns_NotFound_When_Game_Doesnt_Exist()
        {
            A.CallTo(() => gameRepo.Get(1.ToGuid())).Returns(null);

            var actionResult = controller.Guess(1.ToGuid(), 'A') as NotFoundResult;

            Assert.That(actionResult?.StatusCode, Is.EqualTo(404));
        }

        [Test]
        public void Guess_Returns_BadRequest_When_Game_Isnt_InProgress()
        {
            var game = SingleGame();
            game.GameStatus = GameStatus.Won;

            A.CallTo(() => gameRepo.Get(game.Id)).Returns(game);

            var actionResult = controller.Guess(game.Id, 'A') as BadRequestObjectResult;
            var error        = GetPropertyOfAnonymousObject(actionResult?.Value, "error") as string;

            Assert.That(actionResult?.StatusCode, Is.EqualTo(400));
            Assert.That(error, Is.EqualTo("You can only guess in a game which is in progress."));
        }

        [Test]
        public void Guess_Returns_BadRequest_When_Guess_Isnt_A_Letter()
        {
            var game = SingleGame();

            A.CallTo(() => gameRepo.Get(game.Id)).Returns(game);

            var actionResult = controller.Guess(game.Id, '1') as BadRequestObjectResult;
            var error        = GetPropertyOfAnonymousObject(actionResult?.Value, "error") as string;

            Assert.That(actionResult?.StatusCode, Is.EqualTo(400));
            Assert.That(error, Is.EqualTo("You can only guess letters."));
        }

        [Test]
        public void Guess_Returns_BadRequest_When_Guessing_Duplicate_Letter()
        {
            var game = SingleGame();
            game.Guesses.Add(new Guess('A', true, game.Id));

            A.CallTo(() => gameRepo.Get(game.Id)).Returns(game);

            var actionResult = controller.Guess(game.Id, 'A') as BadRequestObjectResult;
            var error        = GetPropertyOfAnonymousObject(actionResult?.Value, "error") as string;

            Assert.That(actionResult?.StatusCode, Is.EqualTo(400));
            Assert.That(error, Is.EqualTo("You already guessed the letter A."));
        }

        [Test]
        public void Guess_Returns_Valid_Result_With_Correct_Letter()
        {
            var game = SingleGame();

            A.CallTo(() => gameRepo.Get(game.Id)).Returns(game);

            var actionResult     = controller.Guess(game.Id, 'A') as OkObjectResult;
            var guessCorrect     = GetPropertyOfAnonymousObject(actionResult?.Value, "guessCorrect") as bool?;
            var word             = GetPropertyOfAnonymousObject(actionResult?.Value, "word") as string;
            var incorrectGuesses = GetPropertyOfAnonymousObject(actionResult?.Value, "incorrectGuessesRemaining") as int?;
            var guesses          = GetPropertyOfAnonymousObject(actionResult?.Value, "guesses") as string;

            Assert.That(actionResult?.StatusCode, Is.EqualTo(200));
            Assert.That(guessCorrect, Is.EqualTo(true));
            Assert.That(word, Is.EqualTo("A _ _ _ _"));
            Assert.That(incorrectGuesses, Is.EqualTo(6));
        }

        [Test]
        public void Guess_Returns_Valid_Result_With_Incorrect_Letter()
        {
            var game = SingleGame();

            A.CallTo(() => gameRepo.Get(game.Id)).Returns(game);

            var actionResult     = controller.Guess(game.Id, 'B') as OkObjectResult;
            var guessCorrect     = GetPropertyOfAnonymousObject(actionResult?.Value, "guessCorrect") as bool?;
            var word             = GetPropertyOfAnonymousObject(actionResult?.Value, "word") as string;
            var incorrectGuesses = GetPropertyOfAnonymousObject(actionResult?.Value, "incorrectGuessesRemaining") as int?;
            var guesses          = GetPropertyOfAnonymousObject(actionResult?.Value, "guesses") as string;

            Assert.That(actionResult?.StatusCode, Is.EqualTo(200));
            Assert.That(guessCorrect, Is.EqualTo(false));
            Assert.That(word, Is.EqualTo("_ _ _ _ _"));
            Assert.That(incorrectGuesses, Is.EqualTo(5));
        }

        [Test]
        public void DeleteGame_Returns_NoContent_On_Success()
        {
            var game = SingleGame();

            A.CallTo(() => gameRepo.Get(game.Id)).Returns(game);

            var actionResult = controller.DeleteGame(game.Id) as NoContentResult;

            Assert.That(actionResult?.StatusCode, Is.EqualTo(204));
        }

        [Test]
        public void DeleteGame_Returns_NotFound_When_Game_Doesnt_Exist()
        {
            A.CallTo(() => gameRepo.Get(1.ToGuid())).Returns(null);

            var actionResult = controller.DeleteGame(1.ToGuid()) as NotFoundResult;

            Assert.That(actionResult?.StatusCode, Is.EqualTo(404));
        }

        #endregion

        #region Helper methods

        private object? GetPropertyOfAnonymousObject(object? anonObj, string propertyName)
        {
            return anonObj?.GetType().GetProperty(propertyName)?.GetValue(anonObj, null);
        }

        private List<Words> WordsList()
        {
            return new List<Words>
            {
                new Words("apple"),
                new Words("banana"),
                new Words("orange")
            };
        }

        private Game SingleGame()
        {
            var game = new Game(new Words("APPLE"));
            game.Id  = 1.ToGuid();
            game.Guesses = new List<Guess>();

            return game;
        }

        private List<Game> GamesList()
        {
            return new List<Game>
            {
                new Game { Id = 1.ToGuid(), GameStatus = GameStatus.InProgress },
                new Game { Id = 2.ToGuid(), GameStatus = GameStatus.Won },
                new Game { Id = 3.ToGuid(), GameStatus = GameStatus.Lost }
            };
        }

        #endregion
    }
}
