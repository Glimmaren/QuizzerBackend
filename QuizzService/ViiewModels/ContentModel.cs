namespace QuizzService.ViiewModels
{
    public class ContentModel
    {
        public string[]? Categories { get; set; }
        public string? Difficulty { get; set; }
        public string? Limit { get; set; }
        public string[]? Tags { get; set; }
        public string? Region { get; set; }

        //public ContentModel(string[]? _category, string? _difficulty, string? _limit, string[]? _tags, string _region)
        //{
        //    Categories = _category;
        //    Difficulty = _difficulty;
        //    Limit = _limit;
        //    Tags = _tags;
        //    Region = _region;
        //}
        //string[] categorys, string difficulty, string limit, string[] tags, string region
    }
}
