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
    public class ProfilePictureController : ControllerBase
    {
        private readonly OPODB _context;

        public ProfilePictureController(OPODB context)
        {
            _context = context;
        }

        // GET: api/ProfilePicture
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProfilePicture>>> GetProfilePicture()
        {
          if (_context.ProfilePicture == null)
          {
              return NotFound();
          }
            return await _context.ProfilePicture.ToListAsync();
        }

        // GET: api/ProfilePicture/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProfilePicture>> GetProfilePicture(int id)
        {
          if (_context.ProfilePicture == null)
          {
              return NotFound();
          }
            var profilePicture = await _context.ProfilePicture.FindAsync(id);

            if (profilePicture == null)
            {
                return NotFound();
            }

            return profilePicture;
        }

        // PUT: api/ProfilePicture/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProfilePicture(int id, ProfilePicture profilePicture)
        {
            if (id != profilePicture.Id)
            {
                return BadRequest();
            }

            _context.Entry(profilePicture).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProfilePictureExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/ProfilePicture
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ProfilePicture>> PostProfilePicture(ProfilePicture profilePicture)
        {
          if (_context.ProfilePicture == null)
          {
              return Problem("Entity set 'OPODB.ProfilePicture'  is null.");
          }
            _context.ProfilePicture.Add(profilePicture);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProfilePicture", new { id = profilePicture.Id }, profilePicture);
        }

        // DELETE: api/ProfilePicture/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProfilePicture(int id)
        {
            if (_context.ProfilePicture == null)
            {
                return NotFound();
            }
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
