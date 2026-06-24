using PracticeProblems.Api.Controllers;
using PracticeProblems.Services.MainServices;
using PracticeProblems.Core.Interfaces;
using PracticeProblems.Data;
using PracticeProblems.Data.Repo;


var builder = WebApplication.CreateBuilder();


// Add services
builder.Services.AddControllers();
builder.Services.AddOpenApi();

// add mongodb
var mongoSettings = builder.Configuration.GetSection("MongoDb").Get<MongoDbSettings>() ?? new MongoDbSettings();
builder.Services.AddSingleton(mongoSettings);
builder.Services.AddSingleton<MongoContext>();

// DI
// add controllers
builder.Services.AddSingleton<ProblemsController>();

// add services
builder.Services.AddSingleton<ProblemsService>();

// add db repo
builder.Services.AddSingleton<IProblemsRepo, ProblemsRepo>();

const string corsPolicyName = "AllowSpecifiedOrigins";
string[] allowedOrigins = { "http://localhost:8080" };


// allow cors for the frontend to access the backend in dev 
builder.Services.AddCors(options =>
{
    options.AddPolicy(corsPolicyName, policy =>
    {   
        policy.WithOrigins(allowedOrigins)
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();

app.UseCors(corsPolicyName);
app.MapControllers();
app.MapOpenApi();
app.Run();






