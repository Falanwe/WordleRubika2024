using Wordle.Models;

namespace Wordle
{
    public interface IWordleService
    {
        Task InitAsync();
        ValueTask<int> StartGame();
        ValueTask<bool> IsAcceptable(string guess);
        ValueTask<GuessResult?> Guess(int gameId, string guess);
    }
}