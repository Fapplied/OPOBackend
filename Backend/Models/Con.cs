namespace Backend.Models;

public class Con
{
    public int ConId { get; set; }

    public string Title { get; set; } = default!;

    public List<UserId>? LikesList { get; set; }
}