using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

List<Carro> carros = new List<Carro>
{
    new Carro { Id = 1, Modelo = "Fusca", Marca = "Volkswagen", Ano = 1985, Placa = "ABC1234" },
    new Carro { Id = 2, Modelo = "Uno", Marca = "Fiat", Ano = 1995, Placa = "DEF5678" },
    new Carro { Id = 3, Modelo = "Celta", Marca = "Chevrolet", Ano = 2005, Placa = "XYZ9876" }
};

app.UseDefaultFiles();
app.UseStaticFiles();


app.MapGet("/carros", () => carros);


app.MapGet("/carros/{id:int}", (int id) =>
{
    var carro = carros.FirstOrDefault(c => c.Id == id);
    return carro is not null ? Results.Ok(carro) : Results.NotFound();
});


app.MapPost("/carros", (Carro carro) =>
{
    carros.Add(carro);
    return Results.Created($"/carros/{carro.Id}", carro);
});


app.MapDelete("/carros/{id:int}", (int id) =>
{
    var carro = carros.FirstOrDefault(c => c.Id == id);
    if (carro is null) return Results.NotFound();
    carros.Remove(carro);
    return Results.Ok(carro);
});

app.Run();

public class Carro
{
    public int Id { get; set; }
    public string Modelo { get; set; }
    public string Marca { get; set; }
    public int Ano { get; set; }
    public string Placa { get; set; }
}
