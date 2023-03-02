using Api;
using Api.Handlers;
using Api.Handlers.Interface;
using Api.Middleware;
using Database;
using Database.Repositories;
using Database.Repositories.Interface;

var builder = WebApplication.CreateBuilder(args);

// Repositories.
builder.Services
    .AddTransient<INodesetRepository, MemoryNodesetRepository>()
    .AddTransient<INodeRepository, MemoryNodeRepository>();

// Handlers
builder.Services
    .AddTransient<INodesetHandler, NodesetHandler>()
    .AddTransient<INodeHandler, NodeHandler>()
    .AddTransient<IPathHandler, PathHandler>();

// Exception Interceptor
builder.Services.AddTransient<ExceptionFilter>();

// Database
builder.Services.AddSingleton<MemoryDatabase>();

// Middleware
builder.Services.AddTransient<RootMiddleware>();

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

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
