using snackapi;
using ticketapi;

namespace userapi;
public class User 
{
    public int Exp { get; set; }
    public string? Sub { get; set; }
    public string? Name { get; set; }
    public string? Preferred_username { get; set; }
    public string? Given_name { get; set; }
    public string? Family_name { get; set; }
    public string? Email { get; set; }
    public Ticket[]? User_tickets { get; set; }
    public Snack[]? User_snacks { get; set; }
}