namespace QuizzerLibary
{
    public class QuestionContent
    {
        public string category { get; set; }
        public string id { get; set; }
        public string correctAnswer { get; set; }
        public string[] incorrectAnswers { get; set; }
        public string question { get; set; }
        public string[] tags { get; set; }
        public string type { get; set; }
        public string difficulty { get; set; }
        public string[] regions { get; set; }
        public bool isNiche { get; set; }
    }
}

        //        {
        //    "category": "Film & TV",
        //    "id": "622a1c377cc59eab6f950730",
        //    "correctAnswer": "Leonardo DiCaprio",
        //    "incorrectAnswers": [
        //      "Ralph Fiennes",
        //      "Brian Cox",
        //      "Kiefer Sutherland"
        //    ],
        //    "question": "Which actor has starred in films including Titanic and The Revenant?",
        //    "tags": [
        //      "acting",
        //      "film",
        //      "film_and_tv"
        //    ],
        //    "type": "Multiple Choice",
        //    "difficulty": "easy",
        //    "regions": [],
        //    "isNiche": false
        //  }
        //}
    

