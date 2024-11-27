namespace ticketapi;

public class Ticket
{
    public int Id { get; set; }
    public int Movie_id  { get; set; }
    public bool? Redeemed { get; set; }
}