using System.Text;
using movieapi;
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

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();