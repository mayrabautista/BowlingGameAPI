using BowlingGame.Core.Domain.Definitions;
using BowlingGame.Core.Domain.Models;
using BowlingGame.Core.Services;
using BowlingGame.Core.Services.Validators;
using BowlingGame.Infraestructure.Mocks;
using FluentValidation;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Services
builder.Services.AddScoped<IGamesService, GamesService>();

// Repositories
builder.Services.AddScoped<IGamesRepository, GamesRepositoryMock>();

// Validators
builder.Services.AddScoped<IValidator<Game>, GameValidator>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
