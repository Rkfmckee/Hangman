namespace Hangman.Blazor.ViewModels
{
    public class GameDetailsViewModel
    {
        public string GameStatus { get; set; }
        public string Word { get; set; }
        public int IncorrectGuessesLeft { get; set; }
        public string Guesses { get; set; }
    }
}
