using Hangman.Enums;

namespace Hangman.Models
{
    public class Game : Entity
    {
        #region Properties

        public string Word { get; set; }

        public string CorrectLetters { get; set; }

        public int IncorrectGuesses { get; set; }

        public GameStatus GameStatus { get; set; }

        #endregion

        #region Relationships

        public List<Guess> Guesses { get; set; }

        #endregion

        #region Constructors

        public Game(string word)
        {
            Word              = word;
            CorrectLetters    = string.Empty;
            IncorrectGuesses  = 0;
            GameStatus        = GameStatus.InProgress;

            for (int i = 0; i < Word.Length; i++)
            {
                CorrectLetters += "_";
            }
        }

        #endregion
    }
}
