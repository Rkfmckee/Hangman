namespace Hangman.Models
{
    public class Words : Entity
    {
        #region Properties

        public string Word { get; set; }

        #endregion

        #region Relationships

        public List<Game> GamesUsed { get; set; }

        #endregion

        #region Constructors

        public Words(string word)
        {
            Word = word;
        }

        #endregion
    }
}
