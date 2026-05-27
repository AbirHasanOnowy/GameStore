using GameStore.API.Data;
using GameStore.API.Dtos;
using GameStore.API.Models;

namespace GameStore.API.Endpoints;

public static class GamesEndpoints
{
    const string GetNameEndpointName = "GetName";

    public static readonly List<GameDto> games =
    [
        new GameDto(1, "Genshin Impact", "Open world action RPG", 0.0M, new DateOnly(2020, 09, 28)),
        new GameDto(
            2,
            "The Legend of Zelda: Breath of the Wild",
            "Open world adventure",
            59.99M,
            new DateOnly(2017, 03, 03)
        ),
        new GameDto(3, "Elden Ring", "Action RPG", 59.99M, new DateOnly(2022, 02, 25)),
        new GameDto(4, "Honkai: Star Rail", "Turn-based RPG", 0.0M, new DateOnly(2023, 04, 26)),
        new GameDto(
            5,
            "Final Fantasy VII Remake",
            "Action RPG",
            69.99M,
            new DateOnly(2020, 04, 10)
        ),
        new GameDto(6, "Monster Hunter: World", "Action RPG", 29.99M, new DateOnly(2018, 01, 26)),
        new GameDto(7, "Cyberpunk 2077", "Open world RPG", 59.99M, new DateOnly(2020, 12, 10)),
        new GameDto(8, "Hades", "Roguelike action", 24.99M, new DateOnly(2020, 09, 17)),
    ];

    public static void MapGamesEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/games");

        //get all games
        group.MapGet("/", (GameStoreContext dbContext) => dbContext.Games.ToList());

        //get games by id
        group
            .MapGet(
                "/{id}",
                (int id) =>
                {
                    var game = games.Find(game => game.Id == id);
                    return game is null ? Results.NotFound("Game not found") : Results.Ok(game);
                }
            )
            .WithName(GetNameEndpointName);

        // Create game
        group.MapPost(
            "/",
            (CreateGameDto newGame, GameStoreContext dbContext) =>
            {
                Game game = new()
                {
                    Id = dbContext.Games.Count() + 1,
                    Name = newGame.Name,
                    GenreId = newGame.GenreId,
                    Price = newGame.Price,
                    ReleaseDate = newGame.ReleaseDate,
                };
                dbContext.Games.Add(game);
                dbContext.SaveChanges();

                GameDetailsDto gameDetails = new(
                    game.Id,
                    game.Name,
                    game.GenreId,
                    game.Price,
                    game.ReleaseDate
                );

                return Results.CreatedAtRoute(
                    GetNameEndpointName,
                    new { id = gameDetails.Id },
                    gameDetails
                );
            }
        );

        // update a game by id
        group.MapPut(
            "/{id}",
            (int id, UpdateGameDto updatedGame) =>
            {
                int index = games.FindIndex(game => game.Id == id);

                if (index == -1)
                    return Results.BadRequest("Gome doesn't exists");

                games[index] = new(
                    id,
                    updatedGame.Name,
                    updatedGame.Genre,
                    updatedGame.Price,
                    updatedGame.ReleaseDate
                );

                return Results.NoContent();
            }
        );

        // delete a game
        group.MapDelete(
            "/{id}",
            (int id) =>
            {
                games.RemoveAll(game => game.Id == id);

                return Results.NoContent();
            }
        );
    }
};
