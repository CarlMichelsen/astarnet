using Api.Configuration;
using Api.Handlers;
using Api.Handlers.Interface;
using Api.Middleware;
using BusinessLogic.Calculators;
using BusinessLogic.Calculators.Interface;
using Database;
using Database.Repositories;
using Database.Repositories.Interface;
using Services;
using Services.Configuration;
using Services.Interface;

var builder = WebApplication.CreateBuilder(args);

// Repositories
builder.Services
    .AddTransient<INodesetRepository, MemoryNodesetRepository>()
    .AddTransient<INodeRepository, MemoryNodeRepository>();

// Handlers
builder.Services
    .AddTransient<INodesetHandler, NodesetHandler>()
    .AddTransient<INodeHandler, NodeHandler>()
    .AddTransient<IPathHandler, PathHandler>();

// Database
builder.Services.AddSingleton<MemoryDatabase>();

// Middleware
builder.Services.AddTransient<RootMiddleware>();

// Calculators
builder.Services
    .AddTransient<IPathCalculator, PathCalculator>();

// Configuration
builder.Services.AddSingleton<IConfiguration>(builder.Configuration);
builder.Services.AddSingleton<IDiscordConfiguration, AppConfiguration>();

builder.Services.AddHttpClient<IDiscordLog, DiscordLog>();

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<RootMiddleware>();

app.UseAuthorization();

app.MapControllers();

app.Run();
