using Backend.Data;

namespace Backend.Service;

public class Problems
{

    private readonly OPODB _context;

    public Problems(OPODB context)
    {
        _context = context;
    }

    public async Task<bool> Delete(int problemId)
    {
        var problem = _context.Problem.Where((problem => problem.ProblemId == problemId)).FirstOrDefault();
        if (problem is null)
        {
            return false;
        }

        var pros = _context.Pro.Where((pro => pro.Problem.ProblemId == problemId));
        var cons =  _context.Con.Where((con => con.Problem.ProblemId == problemId));

        _context.Pro.RemoveRange(pros);
        _context.Con.RemoveRange(cons);

      await _context.SaveChangesAsync();
      var problemLeft = _context.Problem.Where((problem1 => problem1.ProblemId == problemId)).Any();

      return !problemLeft;
    }
}