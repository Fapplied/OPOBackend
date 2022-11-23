namespace Backend.Models;

public class ConsDTO
{
    public int ConId { get; set; }
    public string Title { get; set; } = default!;
    public int ProblemId { get; set; }
    public string ProblemTitle { get; set; } = default!;
    public List<UserId>? LikesList { get; set; }
}