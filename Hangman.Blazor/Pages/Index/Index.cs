using Hangman.Blazor.Interfaces;
using Hangman.Blazor.ViewModels;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

namespace Hangman.Blazor.Pages.Index
{
    public partial class Index
    {
        [CascadingParameter]
        private Task<AuthenticationState> authenticationState { get; set; }

        [Inject]
        private IGameService GameService { get; set; }

        public List<GameViewModel> Games { get; set; } = new List<GameViewModel>();

        public string? Name { get; set; }

        protected async override Task OnInitializedAsync()
        {
            if ((await authenticationState).User.Identity.IsAuthenticated)
            {
                Name = (await authenticationState).User.Identity.Name;
            }

            var games = await GameService.GetAllGames();
            if (games != null && games.Any()) Games = games;
        }
    }
}
