using Microsoft.AspNetCore.Mvc;
using QuizzerLibary;
using QuizzerServer.Interfaces;

namespace QuizzerServer.Controllers
{
    public class FirstToPointController : ControllerBase
    {
        private readonly IFirstToPointGameService _firstToPointGameService;

        public FirstToPointController(IFirstToPointGameService firstToPointGameService)
        {
            _firstToPointGameService = firstToPointGameService;
        }

        [HttpPost("start")]
        public ActionResult<GameState> StartGame()
        {
            var gameState = _firstToPointGameService.StartGame();
            return gameState;
        }

        [HttpGet("question")]
        public QuestionContent GetQuestion() {

            var question =   _firstToPointGameService.GetQuestionAsync(1);
           
            return question;
        }
    }
}
