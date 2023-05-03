using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordleClientAutoImplemented
{
    public class WordleAutoService
    {
        private readonly swaggerClient _swaggerClient;

        public WordleAutoService()
        {
            var httpClient = new HttpClient();
            _swaggerClient = new swaggerClient("https://wordlewebapi20230503163219.azurewebsites.net/", httpClient);
        }

        public async Task<int> Start()
        {
            return await _swaggerClient.StartAsync();
        }

        public async Task<bool> IsValid(string guess)
        {
            return await _swaggerClient.IsValidAsync(guess);
        }

        public async Task<GuessResult> Guess(int gameId, string guess)
        {
            return await _swaggerClient.GuessAsync(gameId, guess);
        }
    }
}
