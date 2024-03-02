using Hangman.API.Helpers;
using Hangman.API.Models;
using Hangman.Helpers;

namespace Hangman.API.ViewModels.Games
{
    public class GameDetailsViewModel
    {
        public string GameStatus { get; set; }
        public string Word { get; set; }
        public int IncorrectGuessesLeft { get; set; }
        public string Guesses { get; set; }

        public GameDetailsViewModel(Game game)
        {
            GameStatus = game.GameStatus.GetDescription();
            Word = game.CorrectLetters.AddSpacesBetweenLetters();
            IncorrectGuessesLeft = game.IncorrectGuessesLeft;
            Guesses = game.Guesses.GetCharsOfGuesses();
        }
    }
}
