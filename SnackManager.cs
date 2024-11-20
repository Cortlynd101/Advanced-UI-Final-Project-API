using System.Text.Json;
namespace snackapi;

public class SnackGetter
{
    public SnackGetter() { }

    public static async Task AddNewSnackAsync(Snack snack)
    {
        string filePath = $"snacks.json";
        string json = await File.ReadAllTextAsync(filePath);
        List<Snack>? snacks = JsonSerializer.Deserialize<List<Snack>>(json);
        snacks?.Add(snack);

        string snacksJson = JsonSerializer.Serialize(snacks);
        await File.WriteAllTextAsync(filePath, snacksJson);
        Console.WriteLine($"JSON data written to {filePath}");
    }

    public static async Task ModifySnackAsync(Snack snack, int id)
    {
        string filePath = $"snacks.json";
        string json = await File.ReadAllTextAsync(filePath);
        List<Snack>? snacks = JsonSerializer.Deserialize<List<Snack>>(json);
        if (snacks is not null)
        {
            snacks[id].Id = snack.Id;
            snacks[id].Name = snack.Name;
            snacks[id].Price = snack.Price;

            string snacksJson = JsonSerializer.Serialize(snacks);
            await File.WriteAllTextAsync(filePath, snacksJson);
            Console.WriteLine($"JSON data modified and written to {filePath}");
        }
        else 
            Console.WriteLine("Snacks are null.");
    }

    public static async void DeleteSnack(int id)
    {
        string filePath = $"snacks.json";
        string json = await File.ReadAllTextAsync(filePath);
        List<Snack>? snacks = JsonSerializer.Deserialize<List<Snack>>(json);
        if (snacks is not null)
            Console.WriteLine($"Snack deleted successfully.");
        snacks?.RemoveAt(id);

        string snacksJson = JsonSerializer.Serialize(snacks);
        await File.WriteAllTextAsync(filePath, snacksJson);
        Console.WriteLine($"JSON data written to {filePath}");
    }

    public static async Task<List<Snack>> GetAllSnacksAsync()
    {
        string filePath = $"snacks.json";
        string json = await File.ReadAllTextAsync(filePath);
        List<Snack>? snacks = JsonSerializer.Deserialize<List<Snack>>(json);
        if (snacks is null)
            throw new Exception("Snacks are null! ");

        if (snacks.Count() == 0)
        {
            Snack snack1 = new Snack() {
                Id = 0,
                Name = "Small Popcorn",
                Price = 3.99M,
            };
            await AddNewSnackAsync(snack1);

            Snack snack2 = new Snack() {
                Id = 1,
                Name = "Medium Popcorn",
                Price = 5.99M,
            };
            await AddNewSnackAsync(snack2);

            Snack snack3 = new Snack() {
                Id = 2,
                Name = "Large Popcorn",
                Price = 7.99M,
            };
            await AddNewSnackAsync(snack3);

            Snack snack4 = new Snack() {
                Id = 3,
                Name = "Jumbo Popcorn",
                Price = 9.99M,
            };
            await AddNewSnackAsync(snack4);

            Snack snack5 = new Snack() {
                Id = 4,
                Name = "Fountain Drink",
                Price = 3.99M,
            };
            await AddNewSnackAsync(snack5);

            Snack snack6 = new Snack() {
                Id = 5,
                Name = "Candy Bar",
                Price = 2.99M,
            };
            await AddNewSnackAsync(snack6);
        }
        else
        {
            return snacks;
        }
        return snacks;
    }
}
