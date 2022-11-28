namespace Backend.Models;

public class ConLike
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int ConId { get; set; }
    public Con Con { get; set; } = default!;

}