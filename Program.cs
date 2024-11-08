using gameapi;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddCors();

var app = builder.Build();
app.UseCors(c => c.AllowAnyHeader()
    .AllowAnyMethod()
.AllowAnyOrigin());

app.MapPost("/add-game", async (Game game) => 
{
    await GameGetter.AddNewGameAsync(game);
    return Results.Ok();
}).DisableAntiforgery();

app.MapPut("/modify-game/{id}", async (Game game, int id) =>
{
    await GameGetter.ModifyGameAsync(game, id);
});

app.MapGet("/", async () =>
{
    var games = await GameGetter.GetAllGamesAsync();
    return games;
});

app.MapDelete("/delete-game/{id}", (int id) =>
{
    GameGetter.DeleteGame(id);
    return Results.Ok();
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();