using Hangman.API.Helpers;
using Hangman.API.Models;
using Hangman.Helpers;

namespace Hangman.API.ViewModels.Games
{
    public class GuessViewModel
    {
        public bool GuessCorrect { get; set; }
        public string Word { get; set; }
        public int IncorrectGuessesLeft { get; }
        public string Guesses { get; set; }

        public GuessViewModel(bool guessCorrect, Game game)
        {
            GuessCorrect = guessCorrect;
            Word = game.CorrectLetters.AddSpacesBetweenLetters();
            IncorrectGuessesLeft = game.IncorrectGuessesLeft;
            Guesses = game.Guesses.GetCharsOfGuesses();
        }
    }
}
