using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Text;
using QuizzerLibary;
using QuizzerServer.Interfaces;

namespace QuizzerServer.Handlers 
{

    public class QuestionHandler : IQuestionHandler
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public QuestionHandler(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }


        public async Task<List<QuestionContent>> GetQuestions(ContentModel model)
        {

            var client = _httpClientFactory.CreateClient();
            string jsonCntent = JsonSerializer.Serialize(model);
            string baseUri = "http://QuizzService/api/questions";
            string query = RequestHandler.TranslateParams(baseUri, model);
            
           
            try
            {
                HttpContent requestContent = new StringContent(jsonCntent, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.GetAsync(query);

                
                var temp = await response.Content.ReadAsStringAsync();
                var questions = JsonSerializer.Deserialize<List<QuestionContent>>(temp);
                return questions;


            }
            
                

            finally { client.Dispose(); }
        }
    }
}
