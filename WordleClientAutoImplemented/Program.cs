using WordleClientAutoImplemented;

var won = false;
var wordleService = new WordleAutoService();



var gameId = await wordleService.Start();

// 6 tentatives
while (true)
{
    string guess;
    while (true)
    {
        guess = Console.ReadLine()!;
        if (!await wordleService.IsValid(guess))
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
    var letterStates = result.LetterStates.ToArray();
    for (var index = 0; index < 5; index++)
    {
        if (letterStates[index] == LetterState.Placed)
        {
            Console.BackgroundColor = ConsoleColor.DarkGreen;
        }
        else if (letterStates[index] == LetterState.Misplaced)
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