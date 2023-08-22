using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizzerLibary
{
    public class CurrentQuestion
    {
        public string questionId { get; set; }
        public string question { get; set; }
        public string[] answers { get; set; }
    }
}
