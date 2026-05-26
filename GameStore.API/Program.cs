using GameStore.API.Data;
using GameStore.API.Endpoints;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddValidation();
builder.Services.AddDbContext<GameStoreContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DbConnectionString"))
);

var app = builder.Build();

//health check
app.MapGet("/", () => "Server is Up and Running!");

app.MapGamesEndpoints();
app.MigrateDb();
app.SeedDatabase();

app.Run();
