using Hangman.Models;

namespace Hangman.API.Helpers
{
    public static class GuessExtensions
    {
        public static string GetCharsOfGuesses(this List<Guess> guesses)
        {
            string characters = string.Empty;

            foreach (var guess in guesses)
            {
                characters += $"{guess.CharacterGuessed}, ";
            }

            characters = characters.Trim().Trim(',');
            return characters;
        }
    }
}
