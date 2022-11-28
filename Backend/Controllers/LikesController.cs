using Backend.Data;
using Backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LikesController : ControllerBase
    {
        private readonly OPODB _context;

        public LikesController(OPODB context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IEnumerable<ProLike>> GetLikes()
        {
            return await _context.ProLike.ToListAsync();
        }

        [HttpGet("pro/{id}")]
        public async Task<ActionResult<IEnumerable<ProLike>>> GetProLikes(int id)
        {
            var pro = _context.Pro
                .Include(r => r.LikesList)
                .SingleOrDefault(r => r.ProId == id);

            if (pro == null)
            {
                return NotFound();
            }

            return pro.LikesList;
        }

        [HttpGet("con/{id}")]
        public async Task<ActionResult<IEnumerable<ConLike>>> GetConLikes(int id)
        {
            var con = _context.Con
                .Include(r => r.LikesList)
                .SingleOrDefault(r => r.ConId == id);

            if (con == null)
            {
                return NotFound();
            }

            return con.LikesList;
        }

        [HttpPost("pro")]
        public async Task<ActionResult<ProLike>> PostProLike(int proId, int userId)
        {
            var pro = _context.Pro
                .Include(r => r.LikesList)
                .Include(r => r.Problem)
                .Single(r => r.ProId == proId);

            var likedAlready = pro.LikesList.SingleOrDefault(r => r.UserId == userId);
            
            if (likedAlready != null)
            {
                var existingLike = await _context.ProLike.FindAsync(likedAlready.Id);
                _context.ProLike.Remove(existingLike);
                await _context.SaveChangesAsync();
                return NoContent();
            }
            
            var like = new ProLike
            {
                UserId = userId
            };

            pro.LikesList?.Add(like);
            await _context.SaveChangesAsync();
            
            return CreatedAtAction("GetProLikes", new { id = userId }, like);
        }

        [HttpPost("con")]
        public async Task<ActionResult<ConLike>> PostConLike(int conId, int userId)
        {
            var con = _context.Con
                .Include(r => r.LikesList)
                .Include(r => r.Problem)
                .Single(r => r.ConId == conId);
            
            var likedAlready = con.LikesList.SingleOrDefault(r => r.UserId == userId);
            
            if (likedAlready != null)
            {
                var existingLike = await _context.ConLike.FindAsync(likedAlready.Id);
                _context.ConLike.Remove(existingLike);
                await _context.SaveChangesAsync();
                return NoContent();
            }

            var like = new ConLike
            {
                UserId = userId
            };

            con.LikesList?.Add(like);

            await _context.SaveChangesAsync();

            return CreatedAtAction("GetConLikes", new { id = userId }, like);
        }
    }
}