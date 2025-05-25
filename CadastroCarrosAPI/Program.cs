using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using CadastroCarrosAPI.Models;
using CadastroCarrosAPI.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Registrar repositório
builder.Services.AddSingleton<CarroRepository>();

// Configurar CORS para liberar acesso do frontend
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

// Usar CORS
app.UseCors("AllowAll");

// Serve arquivos estáticos da wwwroot e index.html por padrão
app.UseDefaultFiles();
app.UseStaticFiles();

// Rotas da API
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
