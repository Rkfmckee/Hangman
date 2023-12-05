using FakeItEasy;
using Hangman.Controllers;
using Hangman.Data;
using Hangman.Data.Interfaces;
using Hangman.Models;
using Microsoft.EntityFrameworkCore;
using Moq;
using Moq.EntityFrameworkCore;

namespace Hangman.Tests
{
    public class GameControllerTests
    {
        [Fact]
        public void CreateGame_Correctly_Creates_A_Game()
        {
            var dbContext = new Mock<ApplicationDbContext>();
            var gameRepo  = A.Fake<IRepository<Game>>();
            var guessRepo = A.Fake<IRepository<Guess>>();
            var wordRepo  = A.Fake<IRepository<Words>>();

            dbContext.Setup<DbSet<Words>>(w => w.Words).ReturnsDbSet(WordsList());
            
            A.CallTo(() => dbContext.Words).Returns();

            var controller = new GameController(dbContext, gameRepo, guessRepo, wordRepo);
        }

        [Fact]
        public void GetGame_Returns_The_Correct_Game()
        {
            var dbContext = new Mock<ApplicationDbContext>();
            var gameRepo = A.Fake<IRepository<Game>>();
            var guessRepo = A.Fake<IRepository<Guess>>();
            var wordRepo = A.Fake<IRepository<Words>>();

            var controller = new GameController(dbContext.Object, gameRepo, guessRepo, wordRepo);
        }

        #region Helper methods

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