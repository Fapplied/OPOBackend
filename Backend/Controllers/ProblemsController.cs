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
    public class ProblemsController : ControllerBase
    {
        private readonly OPODB _context;
        
        private const string URL = "https://api.api-ninjas.com/v1/profanityfilter?text=";


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
                .Include(r => r.ConList)
                .ToListAsync();

            return problem.Select(result => new ProblemDTO
            {
                ProblemId = result.ProblemId,
                UserId = result.User.UserId,
                Title = result.Title,
                ProList = GetPros().Where(r => r.ProblemId == result.ProblemId).ToList(),
                ConList = GetCons().Where(r => r.ProblemId == result.ProblemId).ToList()
            });
        }
        
        [HttpGet("user/{id}")]
        public async Task<IEnumerable<ProblemDTO>> GetUserProblems(int id)
        {        
                var problem = await _context.Problem
                    .Include(r => r.User)
                    .Include(r => r.ProList)
                    .Include(r => r.ConList)
                    .Where(r=>r.User.UserId == id)
                    .ToListAsync();

                return problem.Select(result => new ProblemDTO
                {
                    ProblemId = result.ProblemId,
                    UserId = result.User.UserId,
                    Title = result.Title,
                    ProList = GetPros().Where(r => r.ProblemId == result.ProblemId).ToList(),
                    ConList = GetCons().Where(r => r.ProblemId == result.ProblemId).ToList()
                });
            
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<ProblemDTO>> GetProblem(int id)
        {
            var problem = await _context.Problem
                .Include(r => r.User)
                .Include(r => r.ConList)
                .Include(r => r.ProList)
                .Where(r => r.ProblemId == id).FirstOrDefaultAsync();

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
                ConList = GetCons().Where(r => r.ProblemId == problem.ProblemId).ToList()
            };
        }

        [HttpPost]
        public async Task<ActionResult<Problem>> PostProblem(int userId, AddProblemRequest addProblemRequest)
        {
            var user = _context.User
                .Include(r => r.ProblemList)
                .Single(r => r.UserId == userId);
            
            var client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Add("X-Api-Key", "ki87/fB/+CD6T2m272XIaQ==6N0tIqo70E4D5GFc");
            var safeText =  await  client.GetStreamAsync(URL + addProblemRequest.Title);
            var profanityApiResponse = await JsonSerializer.DeserializeAsync<ProfanityApiResponse>(safeText);


            var problem = new Problem
            {
                Title = profanityApiResponse.Text
            };
        
            user.ProblemList.Add(problem);
            await _context.SaveChangesAsync();
        
            return CreatedAtAction("GetProblem", new { id = problem.ProblemId }, problem);
        }
        
        [HttpDelete("{problemId}")]
        public async Task<IActionResult> DeleteProblem(int problemId)
        {
            
            var problem = _context.Problem.Where((problem => problem.ProblemId == problemId)).FirstOrDefault();
            if (problem is null)
            {
                return NotFound();
            }

            var pros = _context.Pro.Where((pro => pro.Problem.ProblemId == problemId)).ToList();
            var cons =  _context.Con.Where((con => con.Problem.ProblemId == problemId)).ToList();
            if (pros.Any())
            {
                var proLikes = pros.Where(pro => pro.LikesList.FindAll(like => like.ProId == pro.ProId).Any()).Select(pro=>pro.LikesList).ToList();
                if (proLikes.Any())
                {
                    Console.WriteLine("HEEELLLLLOOOOO THEEE PROLIKES IS");
                    Console.WriteLine("LENGTH = " + proLikes.Count() + " First Value is ==> " + proLikes.FirstOrDefault());
                    foreach (var propsLike in proLikes)
                    {
                        _context.Likes.RemoveRange(propsLike);

                    }
                }
                _context.Pro.RemoveRange(pros);
                await _context.SaveChangesAsync();

            }

            if (cons.Any())
            {
                var conLikes = cons.SelectMany(con => con.LikesList).Where(like=> like.ConId > 0 );

                if (conLikes.Any())
                {
                    _context.Likes.RemoveRange(conLikes);
                }
                
                
                _context.Con.RemoveRange(cons);
                await _context.SaveChangesAsync();
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