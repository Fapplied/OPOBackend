namespace Backend.Models;

public class Con
{
    public int ConId { get; set; }
    
    public int UserId { get; set; }

    public string Title { get; set; } = default!;
    
    public Problem Problem { get; set; } = default!;

    public List<ConLike>? LikesList { get; set; }
}