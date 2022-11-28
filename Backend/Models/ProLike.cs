using System.ComponentModel.DataAnnotations;

namespace Backend.Models;

public class ProLike
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int ProId { get; set; }
    public Pro Pro { get; set; } = default!;
}