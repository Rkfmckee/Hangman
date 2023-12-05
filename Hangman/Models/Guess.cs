namespace Hangman.Models
{
    public class Guess : Entity
    {
        #region Properties

        public char CharacterGuessed { get; set; }

        public bool IsCorrect { get; set; }

        #endregion

        #region Relationships

        public Guid GameId { get; set; }

        public Game Game { get; set; }

        #endregion

        #region Constructors

        public Guess(char characterGuessed, bool isCorrect, Guid gameId )
        {
            CharacterGuessed = characterGuessed;
            IsCorrect        = isCorrect;
            GameId           = gameId;
        }

        #endregion
    }
}
