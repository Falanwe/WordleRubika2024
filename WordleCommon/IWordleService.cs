using Wordle.Models;

namespace Wordle
{
    public interface IWordleService
    {
        Task InitAsync();
        int StartGame();
        bool IsAcceptable(string guess);
        GuessResult? Guess(int gameId, string guess);
    }
}