using GameStore.API.Data;
using GameStore.API.Endpoints;
using GameStore.API.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddValidation();
builder.Services.AddDbContext<GameStoreContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DbConnection"))
);

var app = builder.Build();

//health check
app.MapGet("/", () => "Server is Up and Running!");

app.MapGamesEndpoints();
app.MigrateDb();
app.SeedDatabase();

// Seed genre data if empty
// using (var scope = app.Services.CreateScope())
// {
//     var context = scope.ServiceProvider.GetRequiredService<GameStoreContext>();
//     if (!context.Genres.Any())
//     {
//         context.Genres.AddRange(
//             new Genre { Name = "Action" },
//             new Genre { Name = "Adventure" },
//             new Genre { Name = "RPG" },
//             new Genre { Name = "Strategy" },
//             new Genre { Name = "Sports" }
//         );
//         context.SaveChanges();
//     }
//     // Seed game data if empty. Games reference genres seeded above.
//     if (!context.Games.Any())
//     {
//         context.Games.AddRange(
//             new Game
//             {
//                 Name = "Blaze Assault",
//                 GenreId = 3,
//                 Price = 49.99m,
//                 ReleaseDate = new DateOnly(2021, 5, 14),
//             },
//             new Game
//             {
//                 Name = "Island Odyssey",
//                 GenreId = 1,
//                 Price = 39.99m,
//                 ReleaseDate = new DateOnly(2020, 9, 2),
//             },
//             new Game
//             {
//                 Name = "Elder Tales",
//                 GenreId = 2,
//                 Price = 59.99m,
//                 ReleaseDate = new DateOnly(2022, 11, 10),
//             },
//             new Game
//             {
//                 Name = "Empire Tactics",
//                 GenreId = 4,
//                 Price = 29.99m,
//                 ReleaseDate = new DateOnly(2019, 3, 21),
//             },
//             new Game
//             {
//                 Name = "Pro Soccer 22",
//                 GenreId = 5,
//                 Price = 19.99m,
//                 ReleaseDate = new DateOnly(2022, 7, 30),
//             }
//         );
//         context.SaveChanges();
//     }
// }

app.Run();
