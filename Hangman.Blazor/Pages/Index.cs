using Hangman.Blazor.Interfaces;
using Hangman.Blazor.ViewModels;
using Microsoft.AspNetCore.Components;

namespace Hangman.Blazor.Pages
{
    public partial class Index
    {
        [Inject]
        private IGameService GameService { get; set; }

        public List<GameViewModel> Games { get; set; } = new List<GameViewModel>();

        protected async override Task OnInitializedAsync()
        {
            var games = await GameService.GetAllGames();
            if (games != null && games.Any()) Games = games;
        }
    }
}
