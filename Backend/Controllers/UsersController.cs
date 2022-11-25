using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Backend.Data;
using Backend.Models;
using Backend.Service;
using Microsoft.AspNetCore.Cors;

namespace Backend.Controllers
{
    [EnableCors] 
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly OPODB _context;

        public UsersController(OPODB context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            return await _context.User
                .Include(r => r.ProfilePicture)
                .Include(r => r.ProblemList)
                .ToListAsync();
        }
        
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = _context.User
                .Include(r => r.ProfilePicture)
                .Include(r=> r.ProblemList)
                .SingleOrDefault(r => r.UserId == id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        [HttpPost]
        public async Task<ActionResult<User>> PostUser(AddUserRequest addUserRequest)
        {
            var verifyGoogleTokenId = await VerifyToken.VerifyGoogleTokenId(addUserRequest.Token);

            if (verifyGoogleTokenId.Type == "Error")
            {
                return BadRequest(verifyGoogleTokenId);
            }

            var userInfo = _context.User.SingleOrDefault(r => r.GoogleId == addUserRequest.GoogleId);
            // return Ok(test);
            if (userInfo != null)
            {
               return Ok(userInfo);
            }
            
            var user = new User
            {
                GoogleId = addUserRequest.GoogleId,
                Name = addUserRequest.Name
            };
            
            _context.User.Add(user);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetUser", new { id = user.UserId }, user);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _context.User.FindAsync(id);
            
            if (user == null)
            {
                return NotFound();
            }

            _context.User.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}