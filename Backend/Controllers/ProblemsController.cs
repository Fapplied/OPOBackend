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
    public class ProblemsController : ControllerBase
    {
        private readonly OPODB _context;

        public ProblemsController(OPODB context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Problem>>> GetProblem()
        {
            return await _context.Problem.Include(r => r.User).ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Problem>> GetProblem(int id)
        {
            var problem = await _context.Problem.FindAsync(id);

            if (problem == null)
            {
                return NotFound();
            }

            return problem;
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

        private bool ProblemExists(int id)
        {
            return (_context.Problem?.Any(e => e.ProblemId == id)).GetValueOrDefault();
        }
    }
}
