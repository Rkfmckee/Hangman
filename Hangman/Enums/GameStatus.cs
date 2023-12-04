using System.ComponentModel;

namespace Hangman.Enums
{
    public enum GameStatus
    {
        [Description("In progress")]
        InProgress = 0,
        Won = 1,
        Lost = 2
    }
}
