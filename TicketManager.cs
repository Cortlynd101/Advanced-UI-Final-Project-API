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
            Ticket ticket1 = new Ticket() {
                Id = 0,
                Movie_id = 0,
                Redeemed = false,
            };
            await AddNewTicketAsync(ticket1);

            Ticket ticket2 = new Ticket() {
                Id = 1,
                Movie_id = 0,
                Redeemed = false,
            };
            await AddNewTicketAsync(ticket2);

            Ticket ticket3 = new Ticket() {
                Id = 2,
                Movie_id = 1,
                Redeemed = false,
            };
            await AddNewTicketAsync(ticket3);

            Ticket ticket4 = new Ticket() {
                Id = 3,
                Movie_id = 1,
                Redeemed = false,
            };
            await AddNewTicketAsync(ticket4);

            Ticket ticket5 = new Ticket() {
                Id = 4,
                Movie_id = 2,
                Redeemed = false,
            };
            await AddNewTicketAsync(ticket5);

            Ticket ticket6 = new Ticket() {
                Id = 5,
                Movie_id = 2,
                Redeemed = false,
            };
            await AddNewTicketAsync(ticket6);
        }
        else
        {
            return tickets;
        }
        return tickets;
    }
}
