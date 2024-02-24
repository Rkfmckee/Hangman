namespace Hangman.API.ViewModels
{
    public class GuessViewModel
    {
        public bool GuessCorrect { get; set; }
        public string Word { get; set; }
        public int IncorrectGuessesLeft { get; }
        public string Guesses { get; set; }
    }
}
