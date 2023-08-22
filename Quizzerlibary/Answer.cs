using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizzerLibary
{
    public class Answer
    {   
        public string questionid { get; set; }
        public string playerId { get; set; }
        public string answer { get; set; }
        public bool correct { get; set; }
        public int newScore { get; set; }
        public bool isWinner { get; set; }

    }
}
