using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizzerLibary
{
    public class RequestHandler
    {
        public static string TranslateParams(string baseUri,ContentModel model)
        {
            string baseUrl = "https://the-trivia-api.com/api/questions";
            string temp = "";
            if (model.Categories != null && model.Categories.Length > 0)
                temp += $"categories={model.Categories}&";
            if (model.Difficulty != null && model.Difficulty.Length > 0)
                temp += $"difficulty={model.Difficulty}&";
            if (model.Limit != null && model.Limit.Length > 0)
                temp += $"limit={model.Limit}&";
            if (model.Tags != null && model.Tags.Length > 0)
                temp += $"tags={model.Tags}&";
            if (model.Region != null && model.Region.Length > 0)
                temp += $"region={model.Region}&";

            return baseUrl + "?" + temp.Remove(temp.Length - 1, 1);
        }
    }
}
