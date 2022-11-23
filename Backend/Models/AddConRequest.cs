using Microsoft.Build.Framework;

namespace Backend.Models;

public class AddConRequest
{
    [Required]public string Disadvantage { get; set; } = default!;
}