namespace Hangman.Models
{
    public class Words : Entity
    {
        #region Properties

        public string Word { get; set; }

        #endregion

        #region Constructors

        public Words(string word)
        {
                Word = word;
        }

        #endregion
    }
}
