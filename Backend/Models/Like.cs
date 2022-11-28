namespace Backend.Models;

public class Like
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int ConId { get; set; }
    public int ProId { get; set; }
}