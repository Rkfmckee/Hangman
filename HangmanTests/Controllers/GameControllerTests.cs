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
        private IRepository<Words> wordsRepo;

        private GameController controller;

        #endregion

        #region Tests

        [SetUp]
        public void Setup()
        {
            gameRepo = A.Fake<IRepository<Game>>();
            guessRepo = A.Fake<IRepository<Guess>>();
            wordsRepo = A.Fake<IRepository<Words>>();

            controller = new GameController(gameRepo, guessRepo, wordsRepo);
        }

        [Test]
        public void GetGame_Returns_The_Correct_Game()
        {
            var game = new Game
            {
                Id = 1.ToGuid(),
                ChosenWord = new Words("apple"),
                CorrectLetters = string.Empty,
                IncorrectGuessesLeft = 6,
                GameStatus = GameStatus.InProgress,
                Guesses = new List<Guess>()
            };

            A.CallTo(() => gameRepo.Get(game.Id)).Returns(game);

            var actionResult = controller.GetGame(game.Id) as OkObjectResult;
            var gameStatus   = GetPropertyOfAnonymousObject(actionResult?.Value, "gameStatus") as string;
            var word         = GetPropertyOfAnonymousObject(actionResult?.Value, "word") as string;

            Assert.That(actionResult?.StatusCode, Is.EqualTo(200));
            Assert.That(gameStatus, Is.EqualTo("In progress"));
            Assert.That(game.CorrectLetters.AddSpacesBetweenLetters(), Is.EqualTo(word));
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

        #endregion
    }
}
