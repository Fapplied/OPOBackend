using System.Net.Http.Headers;
using System.Text.Json;
using Backend.Models;

namespace Backend.Service;

public static class profanityNinja
{
    private const string URL = "https://api.api-ninjas.com/v1/profanityfilter?text=";

    public static async Task<string> ninja(string inputValue)
    {
        var client = new HttpClient();
        client.DefaultRequestHeaders.Accept.Clear();
        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        client.DefaultRequestHeaders.Add("X-Api-Key", "ki87/fB/+CD6T2m272XIaQ==6N0tIqo70E4D5GFc");
           
        try
        {
            var safeText =  await  client.GetStreamAsync(URL + inputValue);
            var profanityApiResponse = await JsonSerializer.DeserializeAsync<ProfanityApiResponse>(safeText);
            return profanityApiResponse?.Text;
        }
        catch (Exception e)
        {
            return inputValue;
        }
    }
}