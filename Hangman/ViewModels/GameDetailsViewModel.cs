using Hangman.API.Helpers;
using Hangman.Helpers;
using Hangman.Models;

namespace Hangman.API.ViewModels
{
    public class GameDetailsViewModel
    {
        public string GameStatus { get; set; }
        public string Word { get; set; }
        public int IncorrectGuessesLeft { get; }
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
