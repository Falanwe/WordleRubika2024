using Microsoft.AspNetCore.Mvc;
using Wordle;
using Wordle.Models;

namespace WordleWebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WordleController : ControllerBase
    {

        private readonly ILogger<WordleController> _logger;
        private readonly IWordleService _wordleService;

        public WordleController(ILogger<WordleController> logger, IWordleService wordleService)
        {
            _logger = logger;
            _wordleService = wordleService;
        }

        [HttpPost("start")]
        public int StartGame() => _wordleService.StartGame();

        [HttpPost("isValid")]
        public bool IsValid([FromBody] string guess) => _wordleService.IsAcceptable(guess);

        [HttpPost("guess/{gameId}")]
        public ActionResult<GuessResult> Guess(int gameId, [FromBody] string guess)
        {
            if(!_wordleService.IsAcceptable(guess))
            {
                return BadRequest("invalid guess");
            }


            var result = _wordleService.Guess(gameId, guess);

            if(result == null)
            {
                return NotFound("unknown game");
            }
            return result;
        }
    }
}