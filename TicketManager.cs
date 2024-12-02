using System.Text.Json;
namespace ticketapi;

public class TicketGetter
{
    public TicketGetter() { }

    public static async Task AddNewTicketAsync(Ticket ticket)
    {
        string filePath = $"tickets.json";
        string json = await File.ReadAllTextAsync(filePath);
        List<Ticket>? tickets = JsonSerializer.Deserialize<List<Ticket>>(json);
        tickets?.Add(ticket);

        string ticketsJson = JsonSerializer.Serialize(tickets);
        await File.WriteAllTextAsync(filePath, ticketsJson);
        Console.WriteLine($"JSON data written to {filePath}");
    }

    public static async Task ModifyTicketAsync(Ticket ticket, int id)
    {
        string filePath = $"tickets.json";
        string json = await File.ReadAllTextAsync(filePath);
        List<Ticket>? tickets = JsonSerializer.Deserialize<List<Ticket>>(json);
        if (tickets is not null)
        {
            tickets[id].Id = ticket.Id;
            tickets[id].Movie_id = ticket.Movie_id;
            tickets[id].Redeemed = ticket.Redeemed;

            string ticketsJson = JsonSerializer.Serialize(tickets);
            await File.WriteAllTextAsync(filePath, ticketsJson);
            Console.WriteLine($"JSON data modified and written to {filePath}");
        }
        else 
            Console.WriteLine("Tickets are null.");
    }

    public static async void DeleteTicket(int id)
    {
        string filePath = $"tickets.json";
        string json = await File.ReadAllTextAsync(filePath);
        List<Ticket>? tickets = JsonSerializer.Deserialize<List<Ticket>>(json);
        if (tickets is not null)
            Console.WriteLine($"Ticket deleted successfully.");
        tickets?.RemoveAt(id);

        string ticketsJson = JsonSerializer.Serialize(tickets);
        await File.WriteAllTextAsync(filePath, ticketsJson);
        Console.WriteLine($"JSON data written to {filePath}");
    }

    public static async Task<List<Ticket>> GetAllTicketsAsync()
    {
        string filePath = $"tickets.json";
        string json = await File.ReadAllTextAsync(filePath);
        List<Ticket>? tickets = JsonSerializer.Deserialize<List<Ticket>>(json);
        if (tickets is null)
            throw new Exception("Tickets are null! ");

        if (tickets.Count() == 0)
        {
            for (int i = 0; i < 20; i++)
            {
                Ticket newTicket = new Ticket() {
                    Id = i,
                    Movie_id = 0,
                    Redeemed = false
                };
                await AddNewTicketAsync(newTicket);
            }

            for (int i = 0; i < 20; i++)
            {
                Ticket newTicket = new Ticket() {
                    Id = i + 20,
                    Movie_id = 1,
                    Redeemed = false
                };
                await AddNewTicketAsync(newTicket);
            }

            for (int i = 0; i < 20; i++)
            {
                Ticket newTicket = new Ticket() {
                    Id = i + 40,
                    Movie_id = 2,
                    Redeemed = false
                };
                await AddNewTicketAsync(newTicket);
            }
        }
        else
        {
            return tickets;
        }
        return tickets;
    }
}
