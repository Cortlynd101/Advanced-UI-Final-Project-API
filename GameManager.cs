using System.Text.Json;
namespace gameapi;

public class GameGetter
{
    public GameGetter() { }

    public static async Task AddNewGameAsync(Game game)
    {
        string filePath = $"games.json";
        string json = await File.ReadAllTextAsync(filePath);
        List<Game>? games = JsonSerializer.Deserialize<List<Game>>(json);
        games?.Add(game);

        string gamesJson = JsonSerializer.Serialize(games);
        await File.WriteAllTextAsync(filePath, gamesJson);
        Console.WriteLine($"JSON data written to {filePath}");
    }

    public static async Task ModifyGameAsync(Game game, int id)
    {
        string filePath = $"games.json";
        string json = await File.ReadAllTextAsync(filePath);
        List<Game>? games = JsonSerializer.Deserialize<List<Game>>(json);
        if (games is not null)
        {
            games[id].Id = game.Id;
            games[id].Name = game.Name;
            games[id].Genre = game.Genre;
            games[id].HoursPlayed = game.HoursPlayed;
            games[id].HowLongToBeat = game.HowLongToBeat;

            string gamesJson = JsonSerializer.Serialize(games);
            await File.WriteAllTextAsync(filePath, gamesJson);
            Console.WriteLine($"JSON data modified and written to {filePath}");
        }
        else 
            Console.WriteLine("Games are null.");
    }

    public static async void DeleteGame(int id)
    {
        string filePath = $"games.json";
        string json = await File.ReadAllTextAsync(filePath);
        List<Game>? games = JsonSerializer.Deserialize<List<Game>>(json);
        if (games is not null)
            Console.WriteLine($"Game deleted successfully.");
        games?.RemoveAt(id);

        string gamesJson = JsonSerializer.Serialize(games);
        await File.WriteAllTextAsync(filePath, gamesJson);
        Console.WriteLine($"JSON data written to {filePath}");
    }

    public static async Task<List<Game>> GetAllGamesAsync()
    {
        string filePath = $"games.json";
        string json = await File.ReadAllTextAsync(filePath);
        List<Game>? games = JsonSerializer.Deserialize<List<Game>>(json);
        if (games is null)
            throw new Exception("Games are null! ");

        if (games.Count() == 0)
        {
            Game game1 = new Game() {
                Id = 0,
                Name = "Rimworld",
                Genre = "Colony Builder",
                HoursPlayed = "529.6",
                HowLongToBeat = "121"
            };
            await AddNewGameAsync(game1);

            Game game2 = new Game() {
                Id = 1,
                Name = "Terraria",
                Genre = "Open World Survival Craft",
                HoursPlayed = "348.8",
                HowLongToBeat = "150"
            };
            await AddNewGameAsync(game2);

            Game game3 = new Game() {
                Id = 2,
                Name = "Helldivers 2",
                Genre = "Online Co-op",
                HoursPlayed = "241.3",
                HowLongToBeat = "60"
            };
            await AddNewGameAsync(game3);

            Game game4 = new Game() {
                Id = 3,
                Name = "7 Days to Die",
                Genre = "Survival",
                HoursPlayed = "229.8",
                HowLongToBeat = "40"
            };
            await AddNewGameAsync(game4);

            Game game5 = new Game() {
                Id = 4,
                Name = "Counter Strike 2",
                Genre = "FPS",
                HoursPlayed = "192.4",
                HowLongToBeat = "1000"
            };
            await AddNewGameAsync(game5);
        }
        else
        {
            return games;
        }
        return games;
    }
}
