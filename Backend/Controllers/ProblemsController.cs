using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Backend.Data;
using Backend.Models;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProblemsController : ControllerBase
    {
        private readonly OPODB _context;

        public ProblemsController(OPODB context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IEnumerable<ProblemDTO>> GetProblems()
        {
            var problem = await _context.Problem
                .Include(r => r.User)
                .Include(r => r.ProList)
                .Include(r => r.Conlist)
                .ToListAsync();

            return problem.Select(result => new ProblemDTO
            {
                ProblemId = result.ProblemId,
                UserId = result.User.UserId,
                Title = result.Title,
                ProList = GetPros().Where(r => r.ProblemId == result.ProblemId).ToList(),
                Conlist = GetCons().Where(r => r.ProblemId == result.ProblemId).ToList()
            });
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProblemDTO>> GetProblem(int id)
        {
            var problem = _context.Problem
                .Include(r => r.User)
                .Include(r => r.Conlist)
                .Include(r => r.ProList)
                .SingleOrDefault(r => r.ProblemId == id);

            if (problem == null)
            {
                return NotFound();
            }
            
            return new ProblemDTO
            {
                ProblemId = problem.ProblemId,
                UserId = problem.User.UserId,
                Title = problem.Title,
                ProList = GetPros().Where(r => r.ProblemId == problem.ProblemId).ToList(),
                Conlist = GetCons().Where(r => r.ProblemId == problem.ProblemId).ToList()
            };
        }

        [HttpPost]
        public async Task<ActionResult<Problem>> PostProblem(int userId, AddProblemRequest addProblemRequest)
        {
            var user = _context.User.Single(r => r.UserId == userId);
            
            var problem = new Problem
            {
                User = user,
                Title = addProblemRequest.Title
            };

            _context.Problem.Add(problem);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProblem", new { id = problem.ProblemId }, problem);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProblem(int id)
        {
            var problem = await _context.Problem.FindAsync(id);

            if (problem == null)
            {
                return NotFound();
            }

            _context.Problem.Remove(problem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private List<ProDTO> GetPros()
        {
            return _context.Pro.Select(res => new ProDTO
            {
                ProId = res.ProId,
                Title = res.Title,
                ProblemId = res.Problem.ProblemId,
                ProblemTitle = res.Problem.Title,
            }).ToList();
        }

        private List<ConsDTO> GetCons()
        {
            return _context.Con.Select(res => new ConsDTO
            {
                ConId = res.ConId,
                Title = res.Title,
                ProblemId = res.Problem.ProblemId,
                ProblemTitle = res.Problem.Title,
            }).ToList();
        }
    }
}