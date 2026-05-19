using GameStore.API.Dtos;


var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

//health check
app.MapGet("/", () => "Server is Up and Running!");



app.Run();
