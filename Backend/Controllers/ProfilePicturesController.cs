using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Azure.Storage.Blobs.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Backend.Data;
using Backend.Models;
using Backend.Service;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfilePicturesController : ControllerBase
    {
        private readonly OPODB _context;

        public ProfilePicturesController(OPODB context)
        {
            _context = context;
        }

        [HttpGet ("{id}")]
        public async Task<ActionResult<IEnumerable<BlobItem>>> GetProfilePicture(int id)
        {
            var res = await BlobFunctions.Get(id);
            return Ok(res);
        }

        [HttpPost("{id}")]
        public async Task<ActionResult<ProfilePicture>> PostProfilePicture(IFormFile formFile, int id)
        {
           var result = await BlobFunctions.Post(formFile, id);
           
           return CreatedAtAction("PostProfilePicture", new ProfilePicture(){Id = id, Url = result});
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProfilePicture(int id)
        {
            var result = await BlobFunctions.Delete(id);
            if (result.Contains("Deleted"))
            {
                return Ok(result);
            }
            
            return NotFound(result);
            
    }
        
    }
}