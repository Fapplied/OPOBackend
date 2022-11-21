using Microsoft.Build.Framework;

namespace Backend.Models;

public class AddProblemRequest
{
    [Required] public string Title { get; set; } = default!;
}