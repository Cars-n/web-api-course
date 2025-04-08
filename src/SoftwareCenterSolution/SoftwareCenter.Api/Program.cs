using FluentValidation;
using Marten;
using Microsoft.AspNetCore.Http.Json;
using SoftwareCenter.Api.Vendors;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("software") ?? throw new Exception("No connection string! Don't start me");

builder.Services.AddMarten(options =>
{
    options.Connection(connectionString);
}).UseLightweightSessions(); // not worth explaining, Marten specific, unless you are interested ask me later.

// Add services to the container.

// global (other than controllers) for minimal APIs, or for instances of the JSON Serializer like you'll see later.
builder.Services.Configure<JsonOptions>(options =>
{
    options.SerializerOptions.DictionaryKeyPolicy = System.Text.Json.JsonNamingPolicy.CamelCase;
    // this is useful for errors with validation
    // enums
    options.SerializerOptions.DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull;
});

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.DictionaryKeyPolicy = System.Text.Json.JsonNamingPolicy.CamelCase;
    options.JsonSerializerOptions.DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull;
});
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// scoped means a new instance of this thing per http request.
builder.Services.AddScoped<IValidator<CommercialVendorCreate>, CommercialVendorCreateValidator>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/openapi/v1.json", "Software Center");
    });
}

app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program { }