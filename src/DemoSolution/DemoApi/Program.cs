var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddControllers(); // this adds some services associated with activating controllers for requests.

// Everything before this is configuring the backend stuff in our API
var app = builder.Build();
// Everything after this line is configuring "Middleware" - specifically how should HTTP requests be mapped to our code

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.MapGet("/tacos", () => "Lunch Time!");
// Gets controllers ready and reads attributes
app.MapControllers(); // "Reflection"

// Runs api, infinite loop that listens for HTTP requests
app.Run();
