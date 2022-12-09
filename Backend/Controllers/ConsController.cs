using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Backend.Data;
using Backend.Models;
using System.Text.Json;
using System.Net.Http.Headers;
using Backend.Service;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConsController : ControllerBase
    {
        private readonly OPODB _context;
        
        

        public ConsController(OPODB context)
        {
            _context = context;
        }
        
        [HttpGet]
        public async Task<IEnumerable<ConsDTO>> GetCons()
        {
            var con =  await _context.Con
                .Include(r => r.Problem)
                .ToListAsync();
            
            return con.Select(result => new ConsDTO
            {
                ConId = result.ConId,
                Title = result.Title,
                ProblemId = result.Problem.ProblemId,
                ProblemTitle = result.Problem.Title,
            });
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ConsDTO>> GetCon(int id)
        {
            var con = _context.Con
                .Include(r => r.Problem)
                .SingleOrDefault(r => r.ConId == id);

            if (con == null)
            {
                return NotFound();
            }

            return new ConsDTO
            {
                ConId = con.ConId,
                Title = con.Title,
                ProblemId = con.Problem.ProblemId,
                ProblemTitle = con.Problem.Title,
                UserId = con.UserId
            };
        }

        [HttpPost]
        public async Task<ActionResult<Con>> PostCon(int problemId, AddConRequest addConRequest)
        {
            var problem = _context.Problem
                .Include(r => r.ConList)
                .Single(r => r.ProblemId == problemId);
            
            var problemTitleToBeAdded = await profanityNinja.ninja(addConRequest.Disadvantage);
            
            var con = new Con
            {
                Title = problemTitleToBeAdded,
                UserId = addConRequest.UserId
            };
            
            problem.ConList.Add(con);
            await _context.SaveChangesAsync();
            
            return CreatedAtAction("GetCon", new { id = con.ConId }, con);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCon(int id)
        {
            var con = await _context.Con.FindAsync(id);
            
            if (con == null)
            {
                return NotFound();
            }

            _context.Con.Remove(con);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
