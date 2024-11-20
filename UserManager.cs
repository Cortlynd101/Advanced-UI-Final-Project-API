using System.Text.Json;
namespace userapi;

public class UserGetter
{
    public UserGetter() { }

    public static async Task AddNewUserAsync(User user)
    {
        string filePath = $"users.json";
        string json = await File.ReadAllTextAsync(filePath);
        List<User>? users = JsonSerializer.Deserialize<List<User>>(json);
        users?.Add(user);

        string usersJson = JsonSerializer.Serialize(users);
        await File.WriteAllTextAsync(filePath, usersJson);
        Console.WriteLine($"JSON data written to {filePath}");
    }

    public static async Task ModifyUserAsync(User user, int id)
    {
        string filePath = $"users.json";
        string json = await File.ReadAllTextAsync(filePath);
        List<User>? users = JsonSerializer.Deserialize<List<User>>(json);
        if (users is not null)
        {
            users[id].Exp = user.Exp;
            users[id].Name = user.Name;
            users[id].Email = user.Email;

            string usersJson = JsonSerializer.Serialize(users);
            await File.WriteAllTextAsync(filePath, usersJson);
            Console.WriteLine($"JSON data modified and written to {filePath}");
        }
        else 
            Console.WriteLine("Users are null.");
    }

    public static async void DeleteUser(int id)
    {
        string filePath = $"users.json";
        string json = await File.ReadAllTextAsync(filePath);
        List<User>? users = JsonSerializer.Deserialize<List<User>>(json);
        if (users is not null)
            Console.WriteLine($"User deleted successfully.");
        users?.RemoveAt(id);

        string usersJson = JsonSerializer.Serialize(users);
        await File.WriteAllTextAsync(filePath, usersJson);
        Console.WriteLine($"JSON data written to {filePath}");
    }

    public static async Task<List<User>> GetAllUsersAsync()
    {
        string filePath = $"users.json";
        string json = await File.ReadAllTextAsync(filePath);
        List<User>? users = JsonSerializer.Deserialize<List<User>>(json);
        if (users is null)
            throw new Exception("Users are null! ");

        if (users.Count() == 0)
        {
            User user1 = new User() {
                Exp = 0,
                Name = "User1",
                Email = "User1@gmail.com",
            };
            await AddNewUserAsync(user1);

            User user2 = new User() {
                Exp = 1,
                Name = "User2",
                Email = "User2@gmail.com",
            };
            await AddNewUserAsync(user2);

            User user3 = new User() {
                Exp = 2,
                Name = "User3",
                Email = "User3@gmail.com",
            };
            await AddNewUserAsync(user3);
        }
        else
        {
            return users;
        }
        return users;
    }
}
