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


        public Task InitAsync() => Task.CompletedTask;

        public async ValueTask<int> StartGame()
        {
            var httpClient = new HttpClient();
            var postResult = await httpClient.PostAsync("http://10.51.1.240:8080/Wordle/start", JsonContent.Create(""));

            postResult.EnsureSuccessStatusCode();
            return await postResult.Content.ReadFromJsonAsync<int>();
        }

        public async ValueTask<bool> IsAcceptable(string guess)
        {
            var httpClient = new HttpClient();
            var postResult = await httpClient.PostAsync("http://10.51.1.240:8080/Wordle/isValid", JsonContent.Create(guess));

            postResult.EnsureSuccessStatusCode();
            return await postResult.Content.ReadFromJsonAsync<bool>();
        }

        public async ValueTask<GuessResult?> Guess(int gameId, string guess)
        {
            var httpClient = new HttpClient();
            var postResult = await httpClient.PostAsync($"http://10.51.1.240:8080/Wordle/guess/{gameId}", JsonContent.Create(guess));

            postResult.EnsureSuccessStatusCode();
            return await postResult.Content.ReadFromJsonAsync<GuessResult>();
        }





    }
}
