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
    public class ProblemController : ControllerBase
    {
        private readonly OPODB _context;

        public ProblemController(OPODB context)
        {
            _context = context;
        }

        // GET: api/Problem
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Problem>>> GetProblem()
        {
          if (_context.Problem == null)
          {
              return NotFound();
          }
            return await _context.Problem.ToListAsync();
        }

        // GET: api/Problem/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Problem>> GetProblem(int id)
        {
          if (_context.Problem == null)
          {
              return NotFound();
          }
            var problem = await _context.Problem.FindAsync(id);

            if (problem == null)
            {
                return NotFound();
            }

            return problem;
        }

        // PUT: api/Problem/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProblem(int id, Problem problem)
        {
            if (id != problem.ProblemId)
            {
                return BadRequest();
            }

            _context.Entry(problem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProblemExists(id))
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

        // POST: api/Problem
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Problem>> PostProblem(Problem problem)
        {
          if (_context.Problem == null)
          {
              return Problem("Entity set 'OPODB.Problem'  is null.");
          }
            _context.Problem.Add(problem);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProblem", new { id = problem.ProblemId }, problem);
        }

        // DELETE: api/Problem/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProblem(int id)
        {
            if (_context.Problem == null)
            {
                return NotFound();
            }
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
