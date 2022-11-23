namespace Backend.Models;

public class Con
{
    public int ConId { get; set; }

    public string Title { get; set; } = default!;
    
    public Problem Problem { get; set; } = default!;

    public List<Like>? LikesList { get; set; }
}