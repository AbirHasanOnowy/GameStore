using GameStore.API.Endpoints;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddValidation();

var app = builder.Build();

//health check
app.MapGet("/", () => "Server is Up and Running!");

app.MapGamesEndpoints();

app.Run();
