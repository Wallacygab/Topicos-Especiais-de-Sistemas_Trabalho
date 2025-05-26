using CadastroCarrosAPI.Data;

namespace CadastroCarrosAPI.Endpoints
{
    public static class GetCarroById
    {
        public static void MapGetCarroById(this WebApplication app)
        {
            app.MapGet("/api/carros/{id}", async (int id, AppDbContext db) =>
            {
                var carro = await db.Carros.FindAsync(id);
                return carro is not null ? Results.Ok(carro) : Results.NotFound();
            });
        }
    }
}
