using Api;
using Api.Handlers;
using Api.Handlers.Interface;
using Database;
using Database.Repositories;
using Database.Repositories.Interface;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services
    .AddTransient<INodesetHandler, NodesetHandler>()
    .AddTransient<INodesetRepository, MemoryNodesetRepository>()
    .AddTransient<INodeRepository, MemoryNodeRepository>()
    .AddTransient<INodeRepository, MemoryNodeRepository>()
    .AddTransient<ExceptionFilter>();

builder.Services.AddSingleton<MemoryDatabase>();

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

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
