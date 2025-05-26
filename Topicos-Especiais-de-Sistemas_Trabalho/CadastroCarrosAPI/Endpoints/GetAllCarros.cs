using CadastroCarrosAPI.Data;
using Microsoft.EntityFrameworkCore;

namespace CadastroCarrosAPI.Endpoints
{
    public static class GetAllCarros
    {
        public static void MapGetAllCarros(this WebApplication app)
        {
            app.MapGet("/api/carros", async (AppDbContext db) =>
            {
                var carros = await db.Carros.ToListAsync();
                return Results.Ok(carros);
            });
        }
    }
}
