﻿using Hangman.API.Helpers;
using Hangman.Helpers;
using Hangman.Models;

namespace Hangman.API.ViewModels
{
    public class GuessViewModel
    {
        public bool GuessCorrect { get; set; }
        public string Word { get; set; }
        public int IncorrectGuessesLeft { get; }
        public string Guesses { get; set; }

        public GuessViewModel(bool guessCorrect, Game game)
        {
            GuessCorrect = guessCorrect;
            Word = game.CorrectLetters.AddSpacesBetweenLetters();
            IncorrectGuessesLeft = game.IncorrectGuessesLeft;
            Guesses = game.Guesses.GetCharsOfGuesses();
        }
    }
}
