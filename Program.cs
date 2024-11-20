using System.Text;
using movieapi;
using ticketapi;
using snackapi;
using userapi;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddCors();

// builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
//         .AddJwtBearer(options =>
//         {
//             options.TokenValidationParameters = new TokenValidationParameters
//             {
//                 ValidateIssuer = true,
//                 ValidateAudience = true,
//                 ValidateLifetime = true,
//                 ValidateIssuerSigningKey = true,
//                 ValidIssuer = "https://auth.snowse.duckdns.org/realms/advanced-frontend",
//                 ValidAudience = "cort-id",
//                 // IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("your_secret_key"))
//             };
//         });

var app = builder.Build();
app.UseCors(c => c.AllowAnyHeader()
    .AllowAnyMethod()
.AllowAnyOrigin());

app.MapGet("/authOnly", () => {
    Console.WriteLine("In auth endpoint: ");
    return "Hello World Authentication";
});

app.MapGet("/public", () => "Hello World Public!").AllowAnonymous();

app.MapPost("/add-movie", async (Movie movie) => 
{
    await MovieGetter.AddNewMovieAsync(movie);
    return Results.Ok();
}).DisableAntiforgery();

app.MapPut("/modify-movie/{id}", async (Movie movie, int id) =>
{
    await MovieGetter.ModifyMovieAsync(movie, id);
});

app.MapGet("/", async () =>
{
    var movies = await MovieGetter.GetAllMoviesAsync();
    return movies;
});

app.MapDelete("/delete-movie/{id}", (int id) =>
{
    MovieGetter.DeleteMovie(id);
    return Results.Ok();
});

app.MapGet("/get-tickets", async () =>
{
    var tickets = await TicketGetter.GetAllTicketsAsync();
    return tickets;
});

app.MapPost("/add-ticket", async (Ticket ticket) => 
{
    await TicketGetter.AddNewTicketAsync(ticket);
    return Results.Ok();
}).DisableAntiforgery();

app.MapPut("/modify-ticket/{id}", async (Ticket ticket, int id) =>
{
    await TicketGetter.ModifyTicketAsync(ticket, id);
});

app.MapDelete("/delete-ticket/{id}", (int id) =>
{
    TicketGetter.DeleteTicket(id);
    return Results.Ok();
});

app.MapGet("/get-snacks", async () =>
{
    var snacks = await SnackGetter.GetAllSnacksAsync();
    return snacks;
});

app.MapPost("/add-snack", async (Snack snack) => 
{
    await SnackGetter.AddNewSnackAsync(snack);
    return Results.Ok();
}).DisableAntiforgery();

app.MapPut("/modify-snack/{id}", async (Snack snack, int id) =>
{
    await SnackGetter.ModifySnackAsync(snack, id);
});

app.MapDelete("/delete-snack/{id}", (int id) =>
{
    SnackGetter.DeleteSnack(id);
    return Results.Ok();
});

app.MapGet("/get-users", async () =>
{
    var users = await UserGetter.GetAllUsersAsync();
    return users;
});

app.MapPost("/add-user", async (User user) => 
{
    await UserGetter.AddNewUserAsync(user);
    return Results.Ok();
}).DisableAntiforgery();

app.MapPut("/modify-user/{id}", async (User user, int id) =>
{
    await UserGetter.ModifyUserAsync(user, id);
});

app.MapDelete("/delete-user/{id}", (int id) =>
{
    UserGetter.DeleteUser(id);
    return Results.Ok();
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();