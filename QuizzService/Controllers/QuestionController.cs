using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;
using System.Net.Http.Formatting;
using QuizzerLibary;

namespace QuizzService.Controllers
{
    [Route("api/[controller]")]
    public class QuestionController : ControllerBase
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public QuestionController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        public async Task<IActionResult> GetQuestions(ContentModel model)
        {
            // tring[] categories, string difficulty, string limit, string[] tags, string region
            //var model = new ContentModel(categories, difficulty, limit, tags, region);
            string baseUrl = "https://the-trivia-api.com/api/questions";
            //string temp = "";
            //if (model.Categories != null && model.Categories.Length > 0)
            //    temp += $"categories={model.Categories}&";
            //if (model.Difficulty != null && model.Difficulty.Length > 0)
            //    temp += $"difficulty={model.Difficulty}&";
            //if (model.Limit != null && model.Limit.Length > 0)
            //    temp += $"limit={model.Limit}&";
            //if (model.Tags != null && model.Tags.Length > 0) 
            //    temp += $"tags={model.Tags}&";
            //if (model.Region != null && model.Region.Length > 0)
            //    temp += $"region={model.Region}&";

            var query = RequestHandler.TranslateParams(baseUrl, model);
            

            var client = _httpClientFactory.CreateClient();
            var tempResponse = await client.GetAsync(query);
            if (tempResponse == null)
                return StatusCode(400, "Somethiing wnt wrong");

            var response = await tempResponse.Content.ReadAsAsync<List<QuestionContent>>();

            return Ok(response);
        }   

        //TODO: Get Taggar eller kategorier?
    }
}
