using System.Net.Http.Headers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Backend.Data;
using Backend.Models;
using System.Text.Json;


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

        private const string URL = "https://api.api-ninjas.com/v1/profanityfilter?text=";

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
            
            var client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Add("X-Api-Key", "ki87/fB/+CD6T2m272XIaQ==6N0tIqo70E4D5GFc");
            var problemTitleToBeAdded = "";
            try
            {
                var safeText =  await  client.GetStreamAsync(URL + addProRequest.Advantage);
                var profanityApiResponse = await JsonSerializer.DeserializeAsync<ProfanityApiResponse>(safeText);
                problemTitleToBeAdded = profanityApiResponse?.Text;
            }
            catch (Exception e)
            {
                problemTitleToBeAdded = addProRequest.Advantage;
            }
          

            var pro = new Pro
            {
                Title = problemTitleToBeAdded,
                UserId = addProRequest.UserId
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