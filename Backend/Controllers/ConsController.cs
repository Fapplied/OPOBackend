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
    public class ConsController : ControllerBase
    {
        private readonly OPODB _context;

        public ConsController(OPODB context)
        {
            _context = context;
        }

        // GET: api/Con
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Con>>> GetCon()
        {
          if (_context.Con == null)
          {
              return NotFound();
          }
            return await _context.Con.ToListAsync();
        }

        // GET: api/Con/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Con>> GetCon(int id)
        {
          if (_context.Con == null)
          {
              return NotFound();
          }
            var con = await _context.Con.FindAsync(id);

            if (con == null)
            {
                return NotFound();
            }

            return con;
        }

        // PUT: api/Con/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCon(int id, Con con)
        {
            if (id != con.ConId)
            {
                return BadRequest();
            }

            _context.Entry(con).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ConExists(id))
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

        // POST: api/Con
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Con>> PostCon(Con con)
        {
          if (_context.Con == null)
          {
              return Problem("Entity set 'OPODB.Con'  is null.");
          }
            _context.Con.Add(con);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCon", new { id = con.ConId }, con);
        }

        // DELETE: api/Con/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCon(int id)
        {
            if (_context.Con == null)
            {
                return NotFound();
            }
            var con = await _context.Con.FindAsync(id);
            if (con == null)
            {
                return NotFound();
            }

            _context.Con.Remove(con);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ConExists(int id)
        {
            return (_context.Con?.Any(e => e.ConId == id)).GetValueOrDefault();
        }
    }
}
