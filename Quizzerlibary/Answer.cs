using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizzerLibary
{
    public class Answer
    {
        public int PlayerId { get; set; }   
        public string QuestionId { get; set; } 
        public string Text { get; set; }    
        public bool IsCorrect { get; set; }

        public Answer(int playerId, string questionId, string text)
        {
            PlayerId = playerId;
            QuestionId = questionId;
            Text = text;

        }
    }
}
