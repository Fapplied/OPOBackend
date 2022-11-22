using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Models;

public class Pro
{
    public int ProId { get; set; }

    public string Title { get; set; } = default!;

    public List<UserId>? LikesList { get; set; }
}