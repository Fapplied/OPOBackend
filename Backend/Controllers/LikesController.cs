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
        public async Task<IEnumerable<Like>> GetLikes()
        {
            return await _context.Likes.ToListAsync();
        }

        [HttpGet("pro/{id}")]
        public async Task<ActionResult<IEnumerable<Like>>> GetProLikes(int id)
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
        public async Task<ActionResult<IEnumerable<Like>>> GetConLikes(int id)
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
        public async Task<ActionResult<Like>> PostProLike(int proId, int userId)
        {
            var pro = _context.Pro
                .Include(r => r.LikesList)
                .Include(r => r.Problem)
                .Single(r => r.ProId == proId);

            var likedAlready = pro.LikesList.SingleOrDefault(r => r.UserId == userId);
            
            if (likedAlready != null)
            {
                var existingLike = await _context.Likes.FindAsync(likedAlready.Id);
                _context.Likes.Remove(existingLike);
                await _context.SaveChangesAsync();
                return NoContent();
            }
            
            var like = new Like
            {
                UserId = userId
            };

            pro.LikesList?.Add(like);
            await _context.SaveChangesAsync();
            
            return CreatedAtAction("GetProLikes", new { id = userId }, like);
        }

        [HttpPost("con")]
        public async Task<ActionResult<Like>> PostConLike(int conId, int userId)
        {
            var con = _context.Con
                .Include(r => r.LikesList)
                .Include(r => r.Problem)
                .Single(r => r.ConId == conId);
            
            var likedAlready = con.LikesList.SingleOrDefault(r => r.UserId == userId);
            
            if (likedAlready != null)
            {
                var existingLike = await _context.Likes.FindAsync(likedAlready.Id);
                _context.Likes.Remove(existingLike);
                await _context.SaveChangesAsync();
                return NoContent();
            }

            var like = new Like
            {
                UserId = userId
            };

            con.LikesList?.Add(like);

            await _context.SaveChangesAsync();

            return CreatedAtAction("GetConLikes", new { id = userId }, like);
        }
    }
}