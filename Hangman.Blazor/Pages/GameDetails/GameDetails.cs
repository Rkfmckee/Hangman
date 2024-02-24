using Hangman.Blazor.Interfaces;
using Hangman.Blazor.ViewModels;
using Microsoft.AspNetCore.Components;

namespace Hangman.Blazor.Pages.GameDetails
{
    public partial class GameDetails
    {
        [Inject]
        private IGameService GameService { get; set; }

        [Inject]
        private NavigationManager NavigationManager { get; set; }

        [Parameter]
        public Guid Id { get; set; }

        public GameDetailsViewModel Game { get; set; }

        protected async override Task OnInitializedAsync()
        {
            var game = await GameService.GetGame(Id);
            if (game != null) Game = game;
        }
    }
}
