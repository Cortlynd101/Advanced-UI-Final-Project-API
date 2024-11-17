namespace movieapi;

public class Movie
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Genre { get; set; }
    public string? Runtime { get; set; }
    public string? Rating { get; set; }
    public DateTime? EntranceDate { get; set; }
    public DateTime? ExitDate { get; set; }
}