using DaeteckNetworkAPI.Services;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi

//Configure HttpClient for external API calls
builder.Services.AddHttpClient("microtik", client =>
{
    client.BaseAddress = new Uri("http://177.242.140.138/rest/");
});

// Register the client service
builder.Services.AddScoped<IClientService, ClientServices>();

builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
