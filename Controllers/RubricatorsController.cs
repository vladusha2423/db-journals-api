using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Lab5;

namespace Lab5.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RubricatorsController : ControllerBase
    {
        private readonly labsContext _context;

        public RubricatorsController(labsContext context)
        {
            _context = context;
        }

        // GET: api/Rubricators
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Rubricator>>> GetRubricator()
        {
            return await _context.Rubricator.ToListAsync();
        }

        
        [HttpGet("2posts")]
        public async Task<ActionResult<IEnumerable<Rubricator>>> GetRubricatorCount2()
        {
            return await _context.Rubricator
                .Include(r => r.Post)
                .Where(r => r.Post.Count >= 2)
                .ToListAsync();
        }

        // GET: api/Rubricators
        [HttpGet("report")]
        public async Task<ActionResult<IEnumerable<Rubricator>>> GetRubricPost()
        {
            return await _context.Rubricator
                .Include(r => r.Post)
                .ToListAsync();
        }

        // GET: api/Rubricators/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Rubricator>> GetRubricator(string id)
        {
            var rubricator = await _context.Rubricator.FindAsync(id);

            if (rubricator == null)
            {
                return NotFound();
            }

            return rubricator;
        }

        // PUT: api/Rubricators/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRubricator(string id, Rubricator rubricator)
        {
            if (id != rubricator.Code)
            {
                return BadRequest();
            }

            _context.Entry(rubricator).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RubricatorExists(id))
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

        // POST: api/Rubricators
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Rubricator>> PostRubricator(Rubricator rubricator)
        {
            _context.Rubricator.Add(rubricator);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (RubricatorExists(rubricator.Code))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetRubricator", new { id = rubricator.Code }, rubricator);
        }

        // DELETE: api/Rubricators/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Rubricator>> DeleteRubricator(string id)
        {
            var rubricator = await _context.Rubricator.FindAsync(id);
            if (rubricator == null)
            {
                return NotFound();
            }

            _context.Rubricator.Remove(rubricator);
            await _context.SaveChangesAsync();

            return rubricator;
        }

        private bool RubricatorExists(string id)
        {
            return _context.Rubricator.Any(e => e.Code == id);
        }
    }
}
