using Proyecto.DTO;
using Proyecto;
using Proyecto.Data;
using Proyecto.Entities;
using Proyecto.Mapping;
using Microsoft.EntityFrameworkCore;

namespace Proyecto.Endpoints
{
    public static class GamesEndpoints
    {
        const string GetGameEndpointName = "GetGame";
        private static List<GameSummaryDTO> Games = [
          new (
        1,
        "Street Fighter II",
        "Fighting",
        19.99M,
        new DateOnly(1992, 7, 15)),

        new (
        2,
        "Final Fantasy XIV",
        "Roleplaying",
        59.99M,
        new DateOnly(2010, 9, 30)),

        new (
        3,
        "FIFA 23",
        "Sports",
        69.99M,
        new DateOnly(2022, 9, 27)
        )
        ];

        public static RouteGroupBuilder MapGamesEndpoints(this WebApplication app)
        {
            var group = app.MapGroup("/games");

            group.MapGet("/", async (GameStoreContext dbContext) =>
            {
               return await dbContext.Games.Include(game => game.Genre).Select(game => game.ToSummaryDto()).AsNoTracking().ToListAsync();

            });

            group.MapGet("/{id}", async (int id , GameStoreContext dbContext) =>
            {
                GameEntity? game = await dbContext.Games.FindAsync(id);
                
                if (game != null)
                {
                    game.Genre = dbContext.Genres.Find(game.GenreId);
                    return Results.Ok(game.ToSummaryDto());
                }
                else
                {
                    return Results.NotFound();
                }

            }).WithName(GetGameEndpointName);

            group.MapPost("/", async (CreateGameDTO newGame,  GameStoreContext dbContext) =>
            {
                
                GameEntity game = newGame.ToEntity();
               

                dbContext.Games.Add(game);
                await dbContext.SaveChangesAsync();
                
                return Results.CreatedAtRoute(GetGameEndpointName, new { id = game.Id }, game.ToDetailsDto());
            }).WithParameterValidation();

            group.MapPut("/{id}", async (int id, UpdateGameDTO updatedGame, GameStoreContext dbContext) =>
            {
                var ExistingGame = await dbContext.Games.FindAsync(id);

                if (ExistingGame != null)
                {
                    //.EntryLocaliza el juego,.CurrentValues son los valores actuales en la base y con setvalues los reemplaza por el juegoactualizado y lo transforma de DTO a Entidad
                    dbContext.Entry(ExistingGame).CurrentValues.SetValues(updatedGame.toEntity(id));
                    await dbContext.SaveChangesAsync();
                    return Results.NoContent();

                }
                else
                {
                    return Results.NotFound();
                }

            }).WithParameterValidation();

            group.MapDelete("/{id}", async (int id, GameStoreContext dbContext) =>
            {
                GameEntity? game = await dbContext.Games.FindAsync(id);
                if(game != null)
                {
                    dbContext.Games.Remove(game);
                }
                //batch delete(forma eficiente de hacerlo) dbContext.Games.Where(game => games.Id == id).ExecuteDelete();

                await dbContext.SaveChangesAsync();
                return Results.NoContent();
            });

            return group;
        }
    }
}
