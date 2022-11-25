using Google.Apis.Auth;

namespace Backend.Service;

public static class VerifyToken
{
    public static async Task<GoogleJsonWebSignature.Payload>  VerifyGoogleTokenId(string token){    
        try
        { 
            var payload =  await GoogleJsonWebSignature.ValidateAsync(token);
            return payload;
        }
        catch (Exception e)
        {
            return new GoogleJsonWebSignature.Payload()
                { Name = "Invalid_Token_Received", JwtId = $"{e.Message}", Issuer = "AfroCode", Type = "Error"};
        }
    } 
}