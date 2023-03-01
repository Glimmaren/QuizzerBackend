//using QuizzerLibary;

//namespace QuizzerServer
//{
//    public class Temp
//    {
//        public interface IGameService
//        {
//            GameState StartGame();
//            Question GetNextQuestion();
//            void SubmitAnswer(PlayerAnswer playerAnswer);
//            List<PlayerResult> GetResults();
//        }

//        public class GameService : IGameService
//        {
//            private readonly List<Player> _players;
//            private readonly List<Question> _questions;
//            private int _currentQuestionIndex;
//            private GameState _gameState;

//            public GameService(List<Player> players, List<Question> questions)
//            {
//                _players = players;
//                _questions = questions;
//                _currentQuestionIndex = 0;
//                _gameState = GameState.NotStarted;
//            }

//            public GameState StartGame()
//            {
//                _gameState = GameState.InProgress;
//                return _gameState;
//            }

//            public Question GetNextQuestion()
//            {
//                if (_gameState != GameState.InProgress)
//                {
//                    throw new InvalidOperationException("The game is not in progress.");
//                }

//                if (_currentQuestionIndex >= _questions.Count)
//                {
//                    _gameState = GameState.Completed;
//                    return null;
//                }

//                var question = _questions[_currentQuestionIndex];
//                _currentQuestionIndex++;
//                return question;
//            }

//            public void SubmitAnswer(PlayerAnswer playerAnswer)
//            {
//                if (_gameState != GameState.InProgress)
//                {
//                    throw new InvalidOperationException("The game is not in progress.");
//                }

//                var player = _players.FirstOrDefault(p => p.Id == playerAnswer.PlayerId);
//                if (player == null)
//                {
//                    throw new ArgumentException($"Player with ID {playerAnswer.PlayerId} not found.");
//                }

//                var question = _questions.FirstOrDefault(q => q.Id == playerAnswer.QuestionId);
//                if (question == null)
//                {
//                    throw new ArgumentException($"Question with ID {playerAnswer.QuestionId} not found.");
//                }

//                if (question.CorrectAnswer == playerAnswer.Answer)
//                {
//                    player.CorrectAnswers++;
//                }
//            }

//            public List<PlayerResult> GetResults()
//            {
//                if (_gameState != GameState.Completed)
//                {
//                    throw new InvalidOperationException("The game is not completed.");
//                }

//                var results = new List<PlayerResult>();
//                foreach (var player in _players)
//                {
//                    results.Add(new PlayerResult
//                    {
//                        PlayerId = player.Id,
//                        CorrectAnswers = player.CorrectAnswers,
//                        TotalQuestions = _questions.Count
//                    });
//                }

//                return results;
//            }
//        }
//    }
//}
