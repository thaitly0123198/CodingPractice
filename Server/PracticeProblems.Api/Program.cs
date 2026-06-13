var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();


var myAllowOrigins = builder.Configuration.GetSection("allowOrigins").Get<string[]>();    
var myAllowOriginPolicyName = "AllowedSpecifiedOrigins";

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: myAllowOriginPolicyName,
        policy =>
        {
            if (myAllowOrigins != null && myAllowOrigins.Length > 0)        
            {
                policy.WithOrigins(myAllowOrigins)
                    .AllowAnyHeader()
                    .AllowAnyMethod();
            }
            else {throw new InvalidOperationException("No allowed origins recognized in configuration.");}
        });
});
    
    
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseCors(myAllowOriginPolicyName);

app.UseAuthorization();

app.MapControllers();

app.Run();
