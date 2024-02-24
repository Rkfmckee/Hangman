using Hangman.API.ViewModels;
using Hangman.Blazor.ViewModels;

namespace Hangman.Blazor.Interfaces
{
    public interface IGameService
    {
        Task<List<GameViewModel>?> GetAllGames();
        Task<GameDetailsViewModel?> GetGame(Guid id);
        Task<GameViewModel?> CreateGame();
        Task<GuessViewModel?> Guess(SubmitGuessViewModel guessSubmitted);
        Task<bool> Delete(Guid id);
    }
}
