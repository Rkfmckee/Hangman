namespace Hangman.Blazor.ViewModels
{
    public class SubmitGuessViewModel
    {
        public Guid GameId { get; set; }
        public char CharacterGuessed { get; set; }
    }
}
