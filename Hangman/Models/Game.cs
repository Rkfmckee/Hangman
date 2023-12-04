using Hangman.Enums;

namespace Hangman.Models
{
    public class Game : Entity
    {
        #region Properties

        public string Word { get; set; }

        public int IncorrectGuesses { get; set; }

        public GameState GameState { get; set; }

        #endregion

        #region Relationships

        public List<Guess> Guesses { get; set; }

        #endregion
    }
}
