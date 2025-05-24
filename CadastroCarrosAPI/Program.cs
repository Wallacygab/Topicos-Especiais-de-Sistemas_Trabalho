using CadastroCarrosAPI.Data;
using CadastroCarrosAPI.Endpoints;
using CadastroCarrosAPI.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=carros.db"));

var app = builder.Build();

app.MapGetAllCarros();
app.MapGetCarroById();
app.MapPostCarro();
app.MapDeleteCarro();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.EnsureCreated();

    if (!db.Carros.Any())
    {
        db.Carros.AddRange(
            new Carro { Modelo = "Onix", Marca = "Chevrolet", Ano = 2020, Placa = "ABC1234" },
            new Carro { Modelo = "Gol", Marca = "Volkswagen", Ano = 2019, Placa = "DEF5678" },
            new Carro { Modelo = "HB20", Marca = "Hyundai", Ano = 2021, Placa = "GHI9012" },
            new Carro { Modelo = "Corolla", Marca = "Toyota", Ano = 2018, Placa = "JKL3456" },
            new Carro { Modelo = "Civic", Marca = "Honda", Ano = 2022, Placa = "MNO7890" },
            new Carro { Modelo = "Uno", Marca = "Fiat", Ano = 2017, Placa = "PQR2345" },
            new Carro { Modelo = "Ka", Marca = "Ford", Ano = 2016, Placa = "STU6789" },
            new Carro { Modelo = "Sandero", Marca = "Renault", Ano = 2020, Placa = "VWX1122" },
            new Carro { Modelo = "Compass", Marca = "Jeep", Ano = 2023, Placa = "YZA3344" },
            new Carro { Modelo = "Cruze", Marca = "Chevrolet", Ano = 2021, Placa = "BCD5566" }
        );
        db.SaveChanges();
    }
}

app.Run();
