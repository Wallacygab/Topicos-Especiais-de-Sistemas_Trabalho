using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using CadastroCarrosAPI.Models;
using CadastroCarrosAPI.Repositories;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSingleton<CarroRepository>();

var app = builder.Build();

app.MapGet("/", () => "API de Cadastro de Carros");

app.MapGet("/carros", (CarroRepository repo) => repo.GetAll());

app.MapGet("/carros/{id}", (int id, CarroRepository repo) =>
{
    var carro = repo.GetById(id);
    return carro is not null ? Results.Ok(carro) : Results.NotFound();
});

app.MapPost("/carros", (Carro carro, CarroRepository repo) =>
{
    repo.Add(carro);
    return Results.Created($"/carros/{carro.Id}", carro);
});

app.MapPut("/carros/{id}", (int id, Carro carroAtualizado, CarroRepository repo) =>
{
    var sucesso = repo.Update(id, carroAtualizado);
    return sucesso ? Results.Ok(carroAtualizado) : Results.NotFound();
});

app.MapDelete("/carros/{id}", (int id, CarroRepository repo) =>
{
    var sucesso = repo.Delete(id);
    return sucesso ? Results.NoContent() : Results.NotFound();
});

app.Run();

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();

app.UseCors("AllowAll");

// Mapeamentos da API
app.MapGet("/", () => "API de Cadastro de Carros");

// Exemplo:
app.MapGet("/carros", ...);
app.MapPost("/carros", ...);

app.Run();
