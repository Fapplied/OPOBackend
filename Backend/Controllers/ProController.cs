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
    public class ProController : ControllerBase
    {
        private readonly OPODB _context;

        public ProController(OPODB context)
        {
            _context = context;
        }

        // GET: api/Pro
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Pro>>> GetPro()
        {
          if (_context.Pro == null)
          {
              return NotFound();
          }
            return await _context.Pro.ToListAsync();
        }

        // GET: api/Pro/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Pro>> GetPro(int id)
        {
          if (_context.Pro == null)
          {
              return NotFound();
          }
            var pro = await _context.Pro.FindAsync(id);

            if (pro == null)
            {
                return NotFound();
            }

            return pro;
        }

        // PUT: api/Pro/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPro(int id, Pro pro)
        {
            if (id != pro.ProId)
            {
                return BadRequest();
            }

            _context.Entry(pro).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProExists(id))
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

        // POST: api/Pro
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Pro>> PostPro(Pro pro)
        {
          if (_context.Pro == null)
          {
              return Problem("Entity set 'OPODB.Pro'  is null.");
          }
            _context.Pro.Add(pro);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPro", new { id = pro.ProId }, pro);
        }

        // DELETE: api/Pro/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePro(int id)
        {
            if (_context.Pro == null)
            {
                return NotFound();
            }
            var pro = await _context.Pro.FindAsync(id);
            if (pro == null)
            {
                return NotFound();
            }

            _context.Pro.Remove(pro);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProExists(int id)
        {
            return (_context.Pro?.Any(e => e.ProId == id)).GetValueOrDefault();
        }
    }
}
