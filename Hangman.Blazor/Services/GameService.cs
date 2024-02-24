using Hangman.API.ViewModels;
using Hangman.Blazor.Interfaces;
using Hangman.Blazor.ViewModels;
using System.Text;
using System.Text.Json;

namespace Hangman.Blazor.Services
{
    public class GameService : IGameService
    {
        private readonly HttpClient httpClient;

        private JsonSerializerOptions jsonOptions;

        public GameService(HttpClient httpClient)
        {
            this.httpClient = httpClient;

            jsonOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
        }

        public async Task<GameViewModel?> CreateGame()
        {
            var response = await httpClient.PostAsync("api/Game", null);

            if (!response.IsSuccessStatusCode) return null;

            var responseBody = await response.Content.ReadAsStreamAsync();

            return await JsonSerializer.DeserializeAsync<GameViewModel>(responseBody, jsonOptions);
        }

        public async Task<List<GameViewModel>?> GetAllGames()
        {
            var response = await httpClient.GetAsync("api/Game/List");

            if (!response.IsSuccessStatusCode) return null;

            var responseBody = await response.Content.ReadAsStreamAsync();

            return await JsonSerializer.DeserializeAsync<List<GameViewModel>>(responseBody, jsonOptions);
        }

        public async Task<GameDetailsViewModel?> GetGame(Guid id)
        {
            var response = await httpClient.GetAsync($"api/game/{id}");

            if (!response.IsSuccessStatusCode) return null;

            var responseBody = await response.Content.ReadAsStreamAsync();
            var responseString = response.Content.ReadAsStringAsync();

            return await JsonSerializer.DeserializeAsync<GameDetailsViewModel>(responseBody, jsonOptions);
        }

        public async Task<GuessViewModel?> Guess(SubmitGuessViewModel guessSubmitted)
        {
            var request = new StringContent(JsonSerializer.Serialize(guessSubmitted), Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync("api/Game/Guess", request);

            if (!response.IsSuccessStatusCode) return null;
            
            var responseBody = await response.Content.ReadAsStreamAsync();

            return await JsonSerializer.DeserializeAsync<GuessViewModel>(responseBody, jsonOptions);
        }

        public async Task<bool> Delete(Guid id)
        {
            var response = await httpClient.DeleteAsync($"api/Game/{id}");

            return response.IsSuccessStatusCode;
        }
    }
}
