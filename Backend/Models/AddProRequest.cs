using Microsoft.Build.Framework;

namespace Backend.Models;

public class AddProRequest
{
    [Required]public string Advantage { get; set; } = default!;
    [Required]public int UserId { get; set; } = default!;

}