using System.Text;
using gameapi;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddCors();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = "https://auth.snowse.duckdns.org/realms/advanced-frontend",
                ValidAudience = "cort-id",
                // IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("your_secret_key"))
            };
        });

var app = builder.Build();
app.UseCors(c => c.AllowAnyHeader()
    .AllowAnyMethod()
.AllowAnyOrigin());

app.MapGet("/authOnly", () => {
    Console.WriteLine("In auth endpoint: ");
    return "Hello World Authentication";
});

app.MapGet("/public", () => "Hello World Public!").AllowAnonymous();

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