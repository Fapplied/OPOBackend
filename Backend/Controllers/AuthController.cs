using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers;


[AllowAnonymous, Route("[controller]")]
public class Auth:ControllerBase
{
         [Route("google-login")]
         [HttpGet]
        public IActionResult GoogleLogin()
        {
            // var properties = new AuthenticationProperties { RedirectUri = "https://opobackend.azurewebsites.net/auth/google-response" };

            return Challenge(GoogleDefaults.AuthenticationScheme);
        }

        [Route("google-response")]
        [HttpGet]
        public async Task<IActionResult> GoogleResponse()
        {
            try
            {
                var result = await HttpContext.GetTokenAsync(GoogleDefaults.AuthenticationScheme);

                // var claims = result.Principal.Identities.FirstOrDefault()
                //     .Claims.Select(claim => new
                //     {
                //         claim.Properties,
                //         claim.Subject,
                //         claim.Issuer,
                //         claim.OriginalIssuer,
                //         claim.Type,
                //         claim.Value
                //     });

                return Ok(result);
            }
            catch (Exception e)
            {
                return NotFound(e);
            }
        }
    }