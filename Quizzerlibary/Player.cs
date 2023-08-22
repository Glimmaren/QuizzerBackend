using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizzerLibary
{
    public class Player
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int Score { get; set; }
        public bool LastAnswerCorrect { get; set; }
        public bool isWinner { get; set; }

    }
}
