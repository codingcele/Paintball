using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProblemiAPI.Models;

namespace ProblemiAPI.Controllers
{
    [Route("api/Problems")]
    [ApiController]
    public class ProblemsController : ControllerBase
    {
        private readonly ProblemsContext _context;

        public ProblemsController(ProblemsContext context)
        {
            _context = context;
        }

        // GET: api/Problems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProblemDTO>>> GetProblems()
        {
            return await _context.Problems
              .Select(x => ProblemToDTO(x))
              .ToListAsync();
        }

        // GET: api/Problems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProblemDTO>> GetProblem(long id)
        {
            var problem = await _context.Problems.FindAsync(id);

            if (problem == null)
            {
                return NotFound();
            }

            return ProblemToDTO(problem);
        }

        // PUT: api/Problems/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProblem(long id, ProblemDTO problemDTO)
        {
            if (id != problemDTO.Id)
            {
                return BadRequest();
            }

            var problem = await _context.Problems.FindAsync(id);
            if (problem == null)
            {
                return NotFound();
            }

            problem.Name = problemDTO.Name;
            problem.Description = problemDTO.Description;
            problem.PossibleSolution = problemDTO.PossibleSolution;
            problem.IsSolved = problemDTO.IsSolved;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) when (!ProblemExists(id))
            {
                return NotFound();
            }

            return NoContent();
        }

        // POST: api/Problems
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ProblemDTO>> PostProblem(ProblemDTO problemDTO)
        {
            var problem = new Problem
            {
                Name = problemDTO.Name,
                IsSolved = problemDTO.IsSolved,
                Description = problemDTO.Description,
                PossibleSolution = problemDTO.PossibleSolution,
            };

            _context.Problems.Add(problem);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetProblem),
                new { id = problem.Id },
                ProblemToDTO(problem));
        }

        // DELETE: api/Problems/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProblem(long id)
        {
            var problem = await _context.Problems.FindAsync(id);
            if (problem == null)
            {
                return NotFound();
            }

            _context.Problems.Remove(problem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProblemExists(long id)
        {
            return _context.Problems.Any(e => e.Id == id);
        }

        private static ProblemDTO ProblemToDTO(Problem problem) =>
           new ProblemDTO
           {
               Id = problem.Id,
               Name = problem.Name,
               Description = problem.Description,
               PossibleSolution = problem.PossibleSolution,
               IsSolved = problem.IsSolved
           };
    }
}
