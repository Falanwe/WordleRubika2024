using Wordle.Models;

namespace Wordle
{
    public class WordleService : IWordleService
    {
        private string[]? _acceptablewords;
        private readonly List<string> _answers = new();
        private List<(string answer, int remainingGuesses)> _currentGames = new();
        public async Task InitAsync()
        {
            var httpclient = new HttpClient();

            var getAcceptableWordsResult = await httpclient.GetAsync("https://raw.githubusercontent.com/Kinkelin/WordleCompetition/main/data/official/combined_wordlist.txt");
            getAcceptableWordsResult.EnsureSuccessStatusCode();
            _acceptablewords = (await getAcceptableWordsResult.Content.ReadAsStringAsync()).Split('\n').Skip(1).ToArray();

            var getAnwsersResult = await httpclient.GetAsync("https://raw.githubusercontent.com/Kinkelin/WordleCompetition/main/data/official/shuffled_real_wordles.txt");
            getAnwsersResult.EnsureSuccessStatusCode();

            {
                using var reader = new StreamReader(getAnwsersResult.Content.ReadAsStream());
                reader.ReadLine();
                for (var nextWord = reader.ReadLine(); nextWord != null; nextWord = reader.ReadLine())
                {
                    _answers.Add(nextWord);
                }
            }
        }
        public int StartGame()
        {
            var answer = _answers[Random.Shared.Next(_answers.Count)];
            lock (_currentGames)
            {
                _currentGames.Add((answer, 6));
                return _currentGames.Count - 1;
            }
        }

        public bool IsAcceptable(string guess) => _acceptablewords!.Contains(guess);

        public GuessResult? Guess(int gameId, string guess)
        {
            var result = new GuessResult();
            string answer;
            int remainingGuesses;            

            lock (_currentGames)
            {
                if (gameId >= _currentGames.Count)
                {
                    return null;
                }
                (answer, result.RemainingAttempts) = _currentGames[gameId];
                if (result.RemainingAttempts <= 0)
                {
                    result.Answer = answer;
                    return result;
                }
                result.RemainingAttempts--;
                _currentGames[gameId] = (answer, result.RemainingAttempts);
            }



            var lettersInAnswer = answer
    .GroupBy(letter => letter)
    .ToDictionary(group => group.Key, group => group.Count());

            for (var index = 0; index < 5; index++)
            {
                if (guess[index] == answer[index])
                {
                    result.LetterStates[index] = LetterState.Placed;
                    lettersInAnswer[guess[index]]--;
                }
            }

            for (var index = 0; index < 5; index++)
            {
                if (result.LetterStates[index] != LetterState.Placed
                    && lettersInAnswer.TryGetValue(guess[index], out var count)
                    && count > 0)
                {
                    result.LetterStates[index] = LetterState.Misplaced;
                    lettersInAnswer[guess[index]]--;
                }
            }

            if (guess == answer)
            {
                result.Won = true;
            }

            if (result.Won || result.RemainingAttempts == 0)
            {
                result.Answer = answer;
            }

            return result;
        }
    }
}
