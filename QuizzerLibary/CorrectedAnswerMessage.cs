using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizzerLibary
{
    public class CorrectedAnswerMessage
    {
        public string Type { get; set; }
        public string Correctanswer { get; set; }
        public Answer[] Data { get; set; }
        
    }
}
