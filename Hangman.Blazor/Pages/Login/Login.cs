﻿using Hangman.Blazor.Interfaces;
using Hangman.Blazor.ViewModels;
using Microsoft.AspNetCore.Components;

namespace Hangman.Blazor.Pages.Login
{
    public partial class Login
    {
        [Inject]
        public IAuthenticationService AuthenticationService { get; set; }

        private LoginViewModel viewModel { get; set; } = new LoginViewModel();
        private string? errorMessage { get; set; }

        private async Task SubmitAsync()
        {
            try
            {
                await AuthenticationService.LoginAsync(viewModel);
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }
        }
    }
}
