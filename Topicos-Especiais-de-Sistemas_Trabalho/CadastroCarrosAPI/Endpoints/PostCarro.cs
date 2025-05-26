using CadastroCarrosAPI.Data;

namespace CadastroCarrosAPI.Endpoints
{
    public static class PostCarro
    {
        public static void MapPostCarro(this WebApplication app)
        {
            app.MapPost("/api/carros", async (Carro carro, AppDbContext db) =>
            {
                db.Carros.Add(carro);
                await db.SaveChangesAsync();
                return Results.Created($"/api/carros/{carro.Id}", carro);
            });
        }
    }
}
