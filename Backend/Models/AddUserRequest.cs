using Microsoft.Build.Framework;

namespace Backend.Models;

public class AddUserRequest
{
    [Required] public string Name { get; set; }
    [Required] public string GoogleId { get; set; }
}