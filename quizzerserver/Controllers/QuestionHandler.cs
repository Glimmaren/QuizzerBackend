using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Text;
using QuizzerLibary;

namespace QuizzerServer.Controllers
{
    [Route("quizzerserver/api/[controller]")]
    [ApiController]
    public class QuestionController : ControllerBase
    {
        private readonly IHttpClientFactory _httpClientFactory;
    
        public QuestionController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        public async Task<IActionResult> GetQuestion(ContentModel model) {

            var client = _httpClientFactory.CreateClient();
            string jsonCntent = JsonSerializer.Serialize(model);
            string baseUri = "http://QuizzService/api/questions";
            string query = RequestHandler.TranslateParams(baseUri, model);
            
            try
            {
                HttpContent requestContent = new StringContent(jsonCntent, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.GetAsync(query);

                if (response.IsSuccessStatusCode)
                {
                    var temp = await response.Content.ReadAsStringAsync();
                    List<QuestionContent> questions = JsonSerializer.Deserialize<List<QuestionContent>>(temp);
                    return Ok(questions);
                }
                else
                {
                    return BadRequest($"Error getting questions {response}");
                }
            }
            catch (Exception ex)
            {

                return BadRequest("Error getting product: " + ex.Message);
            }
            finally { client.Dispose(); }
        }
    }
}
