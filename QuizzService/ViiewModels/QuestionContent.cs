namespace QuizzService.ViiewModels
{
    public class QuestionContent
    {
        public string Category { get; set; }
        public string Id { get; set; }
        public string CorrectAnswer { get; set; }
        public string[] IncorrectAnswers { get; set; }
        public string Question { get; set; }
        public string[] Tags { get; set; }
        public string Type { get; set; }
        public string Difficulty { get; set; }
        public string[] Regions { get; set; }
        public bool IsNiche { get; set; }


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
    }
}
