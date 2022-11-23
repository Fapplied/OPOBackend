using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Backend.Data;
using Backend.Models;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProsController : ControllerBase
    {
        private readonly OPODB _context;

        public ProsController(OPODB context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IEnumerable<ProDTO>> GetPro()
        {
            var pro = await _context.Pro.Include(r => r.Problem).ToListAsync();
            
            return pro.Select(result => new ProDTO
            {
                ProId = result.ProId,
                Title = result.Title,
                ProblemId = result.Problem.ProblemId,
                ProblemTitle = result.Problem.Title,
            });
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProDTO>> GetPros(int id)
        {
            var pro = _context.Pro.Include(r => r.Problem).SingleOrDefault(r => r.ProId == id);

            if (pro == null)
            {
                return NotFound();
            }

            return new ProDTO
            {
                ProId = pro.ProId,
                Title = pro.Title,
                ProblemId = pro.Problem.ProblemId,
                ProblemTitle = pro.Problem.Title,
            };
        }

        [HttpPost]
        public async Task<ActionResult<Pro>> PostPro(int problemId, AddProRequest addProRequest)
        {
            var problem = _context.Problem
                .Include(r => r.ProList)
                .Single(r => r.ProblemId == problemId);

            var pro = new Pro
            {
                Title = addProRequest.Advantage,
            };
            
            problem.ProList.Add(pro);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPro", new { id = pro.ProId }, pro);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePro(int id)
        {
            var pro = await _context.Pro.FindAsync(id);
            
            if (pro == null)
            {
                return NotFound();
            }

            _context.Pro.Remove(pro);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}