namespace ticketapi;

public class Ticket
{
    public int Id { get; set; }
    public int Movieid  { get; set; }
    public bool? Redeemed { get; set; }
}