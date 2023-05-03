namespace Wordle.Models
{
    public class GuessResult
    {
        public LetterState[] LetterStates { get; set; } = new LetterState[5];
        public int RemainingAttempts { get;set; }
        public string? Answer { get; set; }
        public bool Won { get; set; } = false;
    }
}