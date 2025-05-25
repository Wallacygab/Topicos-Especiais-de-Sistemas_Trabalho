using CadastroCarrosAPI.Models;
using System.Collections.Generic;
using System.Linq;

namespace CadastroCarrosAPI.Repositories;

public class CarroRepository
{
    private readonly List<Carro> _carros = new();
    private int _proximoId = 1;

    public List<Carro> GetAll() => _carros;

    public Carro? GetById(int id) => _carros.FirstOrDefault(c => c.Id == id);

    public void Add(Carro carro)
    {
        carro.Id = _proximoId++;
        _carros.Add(carro);
    }

    public bool Update(int id, Carro carroAtualizado)
    {
        var carro = GetById(id);
        if (carro == null) return false;

        carro.Marca = carroAtualizado.Marca;
        carro.Modelo = carroAtualizado.Modelo;
        carro.Ano = carroAtualizado.Ano;
        return true;
    }

    public bool Delete(int id)
    {
        var carro = GetById(id);
        if (carro == null) return false;

        _carros.Remove(carro);
        return true;
    }
}
