using Wordle;
using Wordle.Models;
using WordleClient;

var won = false;
IWordleService wordleService = new WordleClientService("https://wordlewebapi20230503163219.azurewebsites.net/");

await wordleService.InitAsync();

var gameId = await wordleService.StartGame();

// 6 tentatives
while (true)
{
    string guess;
    while (true)
    {
        guess = Console.ReadLine()!;
        if (!await wordleService.IsAcceptable(guess))
        {
            Console.WriteLine("invalid guess");
        }
        else
        {
            break;
        }
    }

    var result = await wordleService.Guess(gameId, guess);

    Console.ForegroundColor = ConsoleColor.White;
    for (var index = 0; index < 5; index++)
    {
        if (result.LetterStates[index] == LetterState.Placed)
        {
            Console.BackgroundColor = ConsoleColor.DarkGreen;
        }
        else if (result.LetterStates[index] == LetterState.Misplaced)
        {
            Console.BackgroundColor = ConsoleColor.DarkYellow;
        }
        else
        {
            Console.BackgroundColor = ConsoleColor.DarkGray;
        }
        Console.Write(guess[index]);
    }
    Console.ResetColor();
    Console.WriteLine();

    if (result.Won)
    {
        Console.WriteLine("Congratulations");
        break;
    }

    if (result.RemainingAttempts <= 0)
    {
        Console.WriteLine($"Too bad. The word was '{result.Answer}'");
        break;
    }
}

Console.ReadLine();
