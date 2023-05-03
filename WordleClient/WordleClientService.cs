using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Wordle;
using Wordle.Models;

namespace WordleClient
{
    internal class WordleClientService : IWordleService
    {
        private readonly HttpClient _httpClient;

        public WordleClientService(string endpoint)
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(endpoint);
        }

        public Task InitAsync()
        {

            return Task.CompletedTask;
        }

        public async ValueTask<int> StartGame()
        {
            var postResult = await _httpClient.PostAsync("/Wordle/start", JsonContent.Create(""));

            postResult.EnsureSuccessStatusCode();
            return await postResult.Content.ReadFromJsonAsync<int>();
        }

        public async ValueTask<bool> IsAcceptable(string guess)
        {
            var postResult = await _httpClient.PostAsync("/Wordle/isValid", JsonContent.Create(guess));

            postResult.EnsureSuccessStatusCode();
            return await postResult.Content.ReadFromJsonAsync<bool>();
        }

        public async ValueTask<GuessResult?> Guess(int gameId, string guess)
        {
            var postResult = await _httpClient.PostAsync($"/Wordle/guess/{gameId}", JsonContent.Create(guess));

            postResult.EnsureSuccessStatusCode();
            return await postResult.Content.ReadFromJsonAsync<GuessResult>();
        }





    }
}
