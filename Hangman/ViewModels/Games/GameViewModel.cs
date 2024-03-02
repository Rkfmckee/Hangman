using Hangman.Helpers;
using Hangman.Models;

namespace Hangman.API.ViewModels.Games
{
    public class GameViewModel
    {
        public Guid GameId { get; set; }
        public string GameStatus { get; set; }

        public GameViewModel(Game game)
        {
            GameId = game.Id;
            GameStatus = game.GameStatus.GetDescription();
        }
    }
}
