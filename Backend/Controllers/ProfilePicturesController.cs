using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Backend.Data;
using Backend.Models;

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

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProfilePicture>>> GetProfilePicture()
        {
            return await _context.ProfilePicture.ToListAsync();
        }

        [HttpPost("{id}")]
        public async Task<ActionResult<ProfilePicture>> PostProfilePicture(int id)
        {
            // var user = _context.User.Include(r => r.ProfilePicture).SingleOrDefault(r => r.UserId == id);
            // user.ProfilePicture.Url = 
            // await _context.SaveChangesAsync();

            return CreatedAtAction("GetProfilePicture", new { id = id });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProfilePicture(int id)
        {
            var profilePicture = await _context.ProfilePicture.FindAsync(id);
            if (profilePicture == null)
            {
                return NotFound();
            }

            _context.ProfilePicture.Remove(profilePicture);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProfilePictureExists(int id)
        {
            return (_context.ProfilePicture?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}