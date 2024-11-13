using System.Text.Json;
namespace movieapi;

public class MovieGetter
{
    public MovieGetter() { }

    public static async Task AddNewMovieAsync(Movie movie)
    {
        string filePath = $"movies.json";
        string json = await File.ReadAllTextAsync(filePath);
        List<Movie>? movies = JsonSerializer.Deserialize<List<Movie>>(json);
        movies?.Add(movie);

        string moviesJson = JsonSerializer.Serialize(movies);
        await File.WriteAllTextAsync(filePath, moviesJson);
        Console.WriteLine($"JSON data written to {filePath}");
    }

    public static async Task ModifyMovieAsync(Movie movie, int id)
    {
        string filePath = $"movies.json";
        string json = await File.ReadAllTextAsync(filePath);
        List<Movie>? movies = JsonSerializer.Deserialize<List<Movie>>(json);
        if (movies is not null)
        {
            movies[id].Id = movie.Id;
            movies[id].Name = movie.Name;
            movies[id].Genre = movie.Genre;
            movies[id].HoursPlayed = movie.HoursPlayed;
            movies[id].HowLongToBeat = movie.HowLongToBeat;

            string moviesJson = JsonSerializer.Serialize(movies);
            await File.WriteAllTextAsync(filePath, moviesJson);
            Console.WriteLine($"JSON data modified and written to {filePath}");
        }
        else 
            Console.WriteLine("Movies are null.");
    }

    public static async void DeleteMovie(int id)
    {
        string filePath = $"movies.json";
        string json = await File.ReadAllTextAsync(filePath);
        List<Movie>? movies = JsonSerializer.Deserialize<List<Movie>>(json);
        if (movies is not null)
            Console.WriteLine($"Movie deleted successfully.");
        movies?.RemoveAt(id);

        string moviesJson = JsonSerializer.Serialize(movies);
        await File.WriteAllTextAsync(filePath, moviesJson);
        Console.WriteLine($"JSON data written to {filePath}");
    }

    public static async Task<List<Movie>> GetAllMoviesAsync()
    {
        string filePath = $"movies.json";
        string json = await File.ReadAllTextAsync(filePath);
        List<Movie>? movies = JsonSerializer.Deserialize<List<Movie>>(json);
        if (movies is null)
            throw new Exception("Movies are null! ");

        if (movies.Count() == 0)
        {
            Movie movie1 = new Movie() {
                Id = 0,
                Name = "Rimworld",
                Genre = "Colony Builder",
                HoursPlayed = "529.6",
                HowLongToBeat = "121"
            };
            await AddNewMovieAsync(movie1);

            Movie movie2 = new Movie() {
                Id = 1,
                Name = "Terraria",
                Genre = "Open World Survival Craft",
                HoursPlayed = "348.8",
                HowLongToBeat = "150"
            };
            await AddNewMovieAsync(movie2);

            Movie movie3 = new Movie() {
                Id = 2,
                Name = "Helldivers 2",
                Genre = "Online Co-op",
                HoursPlayed = "241.3",
                HowLongToBeat = "60"
            };
            await AddNewMovieAsync(movie3);

            Movie movie4 = new Movie() {
                Id = 3,
                Name = "7 Days to Die",
                Genre = "Survival",
                HoursPlayed = "229.8",
                HowLongToBeat = "40"
            };
            await AddNewMovieAsync(movie4);

            Movie movie5 = new Movie() {
                Id = 4,
                Name = "Counter Strike 2",
                Genre = "FPS",
                HoursPlayed = "192.4",
                HowLongToBeat = "1000"
            };
            await AddNewMovieAsync(movie5);
        }
        else
        {
            return movies;
        }
        return movies;
    }
}
