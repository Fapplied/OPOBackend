using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Models;

public class Con
{
    public int ConId { get; set; }

    public string Title { get; set; } = default!;

    public List<userId>? LikesList { get; set; }
}