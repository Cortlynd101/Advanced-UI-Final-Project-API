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
            movies[id].Runtime = movie.Runtime;
            movies[id].Rating = movie.Rating;
            movies[id].Entrance_date = movie.Entrance_date;
            movies[id].Exit_date = movie.Exit_date;

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
                Name = "Interstellar",
                Genre = "Science Fiction",
                Runtime = "2h 49m",
                Rating = "8.7",
                Entrance_date =  new DateTime(2024, 12, 06),
                Exit_date =  new DateTime(2024, 12, 12),
            };
            await AddNewMovieAsync(movie1);

            Movie movie2 = new Movie() {
                Id = 1,
                Name = "The Dark Knight",
                Genre = "Action",
                Runtime = "2h 32m",
                Rating = "9.0",
                Entrance_date =  new DateTime(2024, 12, 06),
                Exit_date =  new DateTime(2024, 12, 12),
            };
            await AddNewMovieAsync(movie2);

            Movie movie3 = new Movie() {
                Id = 2,
                Name = "The Lord of the Rings: Return of the King",
                Genre = "Fantasy",
                Runtime = "3h 21m",
                Rating = "9.0",
                Entrance_date =  new DateTime(2024, 12, 06),
                Exit_date =  new DateTime(2024, 12, 12),
            };
            await AddNewMovieAsync(movie3);

            Movie movie4 = new Movie() {
                Id = 3,
                Name = "Harry Potter and the Chamber of Secrets",
                Genre = "Fantasy",
                Runtime = "2h 41m",
                Rating = "7.4",
                Entrance_date =  new DateTime(2024, 12, 13),
                Exit_date =  new DateTime(2024, 12, 19),
            };
            await AddNewMovieAsync(movie4);

            Movie movie5 = new Movie() {
                Id = 4,
                Name = "Star Wars: Episode V - The Empire Strikes Back",
                Genre = "Science Fiction",
                Runtime = "2h 4m",
                Rating = "8.7",
                Entrance_date =  new DateTime(2024, 12, 13),
                Exit_date =  new DateTime(2024, 12, 19),
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
