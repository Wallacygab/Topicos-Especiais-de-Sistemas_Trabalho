using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace APIConsultaDeCarro.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CarrosController : ControllerBase
    {
        private static List<Carro> carros = new List<Carro>();
        private static int proximoId = 1;

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(carros);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var carro = carros.FirstOrDefault(c => c.Id == id);
            if (carro == null) return NotFound();
            return Ok(carro);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Carro carro)
        {
            carro.Id = proximoId++;
            carros.Add(carro);
            return CreatedAtAction(nameof(Get), new { id = carro.Id }, carro);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var carro = carros.FirstOrDefault(c => c.Id == id);
            if (carro == null) return NotFound();
            carros.Remove(carro);
            return NoContent();
        }
    }

    public class Carro
    {
        public int Id { get; set; }
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public int Ano { get; set; }
        public string Cor { get; set; }
    }
}
