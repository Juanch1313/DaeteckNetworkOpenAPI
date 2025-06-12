using DaeteckNetworkOpenAPI.Services.ClientService;
using DaeteckNetworkOpenAPI.Services.ODBService;
using DaeteckNetworkOpenAPI.Services.ONUService;
using DaeteckNetworkOpenAPI.Services.ZoneService;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi

//Add CORS policy to allow requests from any origin
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        builder => builder.AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyHeader());
});

// Register the client service
builder.Services.AddScoped<IClientServices, ClientServices>();
builder.Services.AddScoped<IONUServices, ONUServices>();
builder.Services.AddScoped<IZoneServices, ZoneServices>();
builder.Services.AddScoped<IODBServices, ODBServices>();

builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();

app.UseCors();

app.UseAuthorization();

app.MapControllers();

app.Run();
