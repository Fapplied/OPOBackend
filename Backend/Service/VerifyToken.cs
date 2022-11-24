using Google.Apis.Auth;

namespace Backend.Service;

public static class VerifyToken
{
    public static async Task<GoogleJsonWebSignature.Payload>  VerifyGoogleTokenId(string token){    
        try
        {  
            // uncomment these lines if you want to add settings: 
            // var validationSettings = new GoogleJsonWebSignature.ValidationSettings
            // { 
            //     Audience = new string[] { "yourServerClientIdFromGoogleConsole.apps.googleusercontent.com" }
            // };
            // Add your settings and then get the payload
            // GoogleJsonWebSignature.Payload payload =  await GoogleJsonWebSignature.ValidateAsync(token, validationSettings);

            // Or Get the payload without settings.
            token += "lot of wierd";
            var payload =  await GoogleJsonWebSignature.ValidateAsync(token);
            
            // if (payload is null)
            // {
            //     
            // }
            return payload;
        }
        catch (Exception)
        {
            
            throw new ArgumentException("Missing Legit Token");
            Console.WriteLine("invalid google token");

        }
        return null;
    } 
}