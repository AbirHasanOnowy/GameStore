using GameStore.API.Models;
using Microsoft.EntityFrameworkCore;

namespace GameStore.API.Data;

// Extension method to apply pending migrations at application startup

public static class DataExtensions
{
    public static void MigrateDb(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<GameStoreContext>();
        dbContext.Database.Migrate();
    }

    public static void SeedDatabase(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<GameStoreContext>();

        // Seed genre data if empty
        if (!context.Genres.Any())
        {
            context.Genres.AddRange(
                new Genre { Name = "Action" },
                new Genre { Name = "Adventure" },
                new Genre { Name = "RPG" },
                new Genre { Name = "Strategy" },
                new Genre { Name = "Sports" }
            );
            context.SaveChanges();
        }

        // Seed game data if empty. Games reference genres seeded above.
        if (!context.Games.Any())
        {
            context.Games.AddRange(
                new Game
                {
                    Name = "Blaze Assault",
                    GenreId = 3,
                    Price = 49.99m,
                    ReleaseDate = new DateOnly(2021, 5, 14),
                },
                new Game
                {
                    Name = "Island Odyssey",
                    GenreId = 1,
                    Price = 39.99m,
                    ReleaseDate = new DateOnly(2020, 9, 2),
                },
                new Game
                {
                    Name = "Elder Tales",
                    GenreId = 2,
                    Price = 59.99m,
                    ReleaseDate = new DateOnly(2022, 11, 10),
                },
                new Game
                {
                    Name = "Galactic Conquest",
                    GenreId = 4,
                    Price = 29.99m,
                    ReleaseDate = new DateOnly(2019, 3, 22),
                },
                new Game
                {
                    Name = "Pro Sports League",
                    GenreId = 5,
                    Price = 19.99m,
                    ReleaseDate = new DateOnly(2021, 7, 30),
                }
            );
            context.SaveChanges();
        }
    }
}
