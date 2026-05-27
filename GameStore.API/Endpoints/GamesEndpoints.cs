using GameStore.API.Data;
using GameStore.API.Dtos;
using GameStore.API.Models;
using Microsoft.EntityFrameworkCore;

namespace GameStore.API.Endpoints;

public static class GamesEndpoints
{
    const string GetNameEndpointName = "GetName";

    public static void MapGamesEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/games");

        //get all games
        group.MapGet(
            "/",
            async (GameStoreContext dbContext) =>
            {
                return Results.Ok(
                    await dbContext
                        .Games.Include(game => game.Genre)
                        .Select(game => new GameSummaryDto(
                            game.Id,
                            game.Name,
                            game.Genre!.Name,
                            game.Price,
                            game.ReleaseDate
                        ))
                        .AsNoTracking() // AsNoTracking is used to improve performance when we don't need to track changes to the entities
                        .ToListAsync()
                );
            }
        );

        //get games by id
        group
            .MapGet(
                "/{id}",
                async (int id, GameStoreContext dbContext) =>
                {
                    var game = await dbContext.Games.FindAsync(id);
                    return game is null
                        ? Results.NotFound("Game not found")
                        : Results.Ok(
                            new GameDetailsDto(
                                game.Id,
                                game.Name,
                                game.GenreId,
                                game.Price,
                                game.ReleaseDate
                            )
                        );
                }
            )
            .WithName(GetNameEndpointName);

        // Create game
        group.MapPost(
            "/",
            async (CreateGameDto newGame, GameStoreContext dbContext) =>
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
                await dbContext.SaveChangesAsync();

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
            async (int id, UpdateGameDto updatedGame, GameStoreContext dbContext) =>
            {
                var game = await dbContext.Games.FindAsync(id);

                if (game is null)
                    return Results.BadRequest("Game doesn't exist");

                game.Name = updatedGame.Name;
                game.GenreId = updatedGame.GenreId;
                game.Price = updatedGame.Price;
                game.ReleaseDate = updatedGame.ReleaseDate;

                await dbContext.SaveChangesAsync();

                return Results.NoContent();
            }
        );

        // delete a game
        group.MapDelete(
            "/{id}",
            async (int id, GameStoreContext dbContext) =>
            {
                await dbContext.Games.Where(game => game.Id == id).ExecuteDeleteAsync();
                // ExecuteDeleteAsync is used to delete entities without loading them into memory, which improves performance

                return Results.NoContent();
            }
        );
    }
};
