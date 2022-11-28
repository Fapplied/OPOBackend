namespace Backend.Models;

public class ProDTO
{
    public int ProId { get; set; }
    public string Title { get; set; } = default!;
    public int ProblemId { get; set; }
    public string ProblemTitle { get; set; } = default!;
    public List<ProLike>? LikesList { get; set; }
}