namespace Backend.Models;
using System.Text.Json.Serialization;

public class ProfanityApiResponse
{
    [JsonPropertyName("censored")]
    public string Text { get; set; }
    
}