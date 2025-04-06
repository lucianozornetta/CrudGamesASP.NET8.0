using Proyecto.DTO;
using Proyecto;

namespace Proyecto.Endpoints
{
    public static class GamesEndpoints
    {
        const string GetGameEndpointName = "GetGame";
        private static List<GameDTO> Games = [
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

            group.MapGet("/", () => Games);

            group.MapGet("/{id}", (int id) =>
            {
                var game = Games.Find(game => game.Id == id);
                if (game != null)
                {
                    return Results.Ok(game);
                }
                else
                {
                    return Results.NotFound();
                }

            }).WithName(GetGameEndpointName);

            group.MapPost("/", (CreateGameDTO newGame) =>
            {
                GameDTO game = new GameDTO(Games.Count + 1, newGame.Name, newGame.Genre, newGame.Price, newGame.ReleaseDate);
                Games.Add(game);

                return Results.CreatedAtRoute(GetGameEndpointName, new { id = game.Id }, game);
            }).WithParameterValidation();

            group.MapPut("/{id}", (int id, UpdateGameDTO updatedGame) =>
            {
                var index = Games.FindIndex(game => game.Id == id);
                if (index != -1)
                {
                    Games[index] = new GameDTO(id, updatedGame.Name, updatedGame.Genre, updatedGame.Price, updatedGame.ReleaseDate);

                    return Results.NoContent();
                }
                else
                {
                    return Results.NotFound();
                }

            }).WithParameterValidation();

            group.MapDelete("/games/{id}", (int id) =>
            {
                Games.RemoveAll(game => game.Id == id);
                return Results.NoContent();
            });

            return group;
        }
    }
}
