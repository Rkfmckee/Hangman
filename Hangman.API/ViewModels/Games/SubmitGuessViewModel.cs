namespace Hangman.API.ViewModels.Games
{
    public class SubmitGuessViewModel
    {
        public Guid GameId { get; set; }
        public char CharacterGuessed { get; set; }
    }
}
