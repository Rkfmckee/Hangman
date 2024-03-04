using Hangman.Blazor.ViewModels;
using Microsoft.AspNetCore.Components;

namespace Hangman.Blazor.Pages.Index.Components.GamesTable
{
    public partial class GamesTable
    {
        [Parameter]
        public List<GameViewModel> Games { get; set; }
    }
}
