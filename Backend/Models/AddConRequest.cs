using Microsoft.Build.Framework;

namespace Backend.Models;

public class AddConRequest
{
    [Required]public string Disadvantage { get; set; } = default!;
    [Required]public int UserId { get; set; } = default!;

}