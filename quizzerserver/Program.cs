using QuizzerLibary;
using QuizzerServer.Communication;
using QuizzerServer.Data;
using QuizzerServer.Handlers;
using QuizzerServer.Interfaces;
using WebSocketSharp.Server;
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
//var server = new WebsocketServer();
var server = new WebSocketServer(5000);
//var contentModel = new ContentModel();
//contentModel.Limit = "100";
server.Start(); //"ws://0.0.0.0:5000"
server.AddWebSocketService("/firsttopointservice", () => new SocketHandler(questionHandler));
//server.AddService<FirstToPointGameService, IQuestionHandler>("/firsttopointservice", questionHandler);



app.Run();
