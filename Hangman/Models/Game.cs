using Hangman.Enums;

namespace Hangman.API.Models
{
    public class Game : Entity
    {
        #region Properties

        public string CorrectLetters { get; set; }

        public int IncorrectGuessesLeft { get; set; }

        public GameStatus GameStatus { get; set; }

        #endregion

        #region Relationships

        public Guid ChosenWordId { get; set; }
        public Words ChosenWord { get; set; }

        public List<Guess> Guesses { get; set; }

        #endregion

        #region Constructors

        public Game()
        {
        }

        public Game(Words word)
        {
            ChosenWord = word;
            CorrectLetters = string.Empty;
            IncorrectGuessesLeft = 6;
            GameStatus = GameStatus.InProgress;

            for (int i = 0; i < ChosenWord.Word.Length; i++)
            {
                CorrectLetters += "_";
            }
        }

        #endregion
    }
}
