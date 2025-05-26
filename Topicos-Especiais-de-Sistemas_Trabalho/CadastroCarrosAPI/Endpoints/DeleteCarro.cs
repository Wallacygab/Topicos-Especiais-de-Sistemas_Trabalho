using CadastroCarrosAPI.Data;

namespace CadastroCarrosAPI.Endpoints
{
    public static class DeleteCarro
    {
        public static void MapDeleteCarro(this WebApplication app)
        {
            app.MapDelete("/api/carros/{id}", async (int id, AppDbContext db) =>
            {
                var carro = await db.Carros.FindAsync(id);
                if (carro is null) return Results.NotFound();
                db.Carros.Remove(carro);
                await db.SaveChangesAsync();
                return Results.NoContent();
            });
        }
    }
}
