using Hangman.Blazor.ViewModels;
using Microsoft.AspNetCore.Components;

namespace Hangman.Blazor.Pages.GameDetails.Components.GameDetailsList
{
    public partial class GameDetailsList
    {
        [Parameter]
        public GameDetailsViewModel Game { get; set; }
    }
}
