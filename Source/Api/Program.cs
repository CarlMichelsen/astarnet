using Astar.Api.Configuration;
using Astar.Api.Handlers;
using Astar.Api.Handlers.Interface;
using Astar.Api.Middleware;
using Astar.BusinessLogic.Calculators;
using Astar.BusinessLogic.Calculators.Interface;
using Astar.Database;
using Astar.Database.Repositories;
using Astar.Database.Repositories.Interface;
using Astar.Services;
using Astar.Services.Configuration;
using Astar.Services.Interface;

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
