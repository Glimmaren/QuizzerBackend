using QuizzerLibary;
using QuizzerServer.Data;
using QuizzerServer.Handlers;
using QuizzerServer.Interfaces;
using WebSokcetLibary;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddHttpClient<IQuestionHandler, QuestionHandler>();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    
}

app.UseHttpsRedirection();
app.UseWebSockets();

app.UseAuthorization();

app.MapControllers();

var httpClientFactory = app.Services.GetRequiredService<IHttpClientFactory>();
var questionHandler = new QuestionHandler(httpClientFactory);
var server = new WebsocketServer();
var contentModel = new ContentModel();
contentModel.Limit = "20";
server.Start("ws://0.0.0.0:5000");
server.AddService<FirstToPointGameService, IQuestionHandler>("/firsttopointservice", questionHandler);

app.Run();
