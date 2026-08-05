using PracticeProblems.Api.Controllers;
using PracticeProblems.Services.MainServices;
using PracticeProblems.Services.FileManip;
using PracticeProblems.Core.Interfaces;
using PracticeProblems.Data;
using PracticeProblems.Data.Repo;


var builder = WebApplication.CreateBuilder(args);

// Railway inject PORT. Local Docker defaults to 8080.
var port = Environment.GetEnvironmentVariable("PORT") ?? "8080";
builder.WebHost.UseUrls($"http://0.0.0.0:{port}");

builder.Services.AddControllers();
builder.Services.AddOpenApi();

//  mongodb
var mongoSettings = builder.Configuration.GetSection("MongoDb").Get<MongoDbSettings>() ?? new MongoDbSettings();
builder.Services.AddSingleton(mongoSettings);
builder.Services.AddSingleton<MongoContext>();

// DI
// add controllers
builder.Services.AddSingleton<ProblemsController>();

// add services
builder.Services.AddSingleton<ProblemsService>();
builder.Services.AddSingleton<IJudge, JudgeService>();
builder.Services.AddTransient<SolutionFileManagement>();
builder.Services.AddTransient<ProcessManagement>();


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






