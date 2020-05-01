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
    public class JournalsController : Controller
    {
        private readonly labsContext _context;

        public JournalsController(labsContext context)
        {
            _context = context;
        }

        // GET: api/Journals
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Journal>>> GetJournal()
        {
            return await _context.Journal
                .ToListAsync();
        }

        // GET: api/Journals
        [HttpGet("report")]
        public async Task<ActionResult<IEnumerable<Journal>>> GetJournalReleasePost()
        {
            return await _context.Journal
                .Include(j => j.Release)
                .ThenInclude(r => r.Post)
                .ToListAsync();
        }

        
        [HttpGet("2020")]
        public async Task<ActionResult<IEnumerable<Journal>>> GetJournal2020()
        {
            return await _context.Journal
                .Include(j => j.Release)
                .Where(j => j.Release.Any(r => r.Year == 2020))
                .ToListAsync();
        }

        // GET: api/Journals/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Journal>> GetJournal(decimal id)
        {
            var journal = await _context.Journal.FindAsync(id);

            if (journal == null)
            {
                return NotFound();
            }

            return journal;
        }

        // PUT: api/Journals/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutJournal(decimal id, Journal journal)
        {
            if (id != journal.Index)
            {
                return BadRequest();
            }

            _context.Entry(journal).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!JournalExists(id))
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

        // POST: api/Journals
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Journal>> PostJournal(Journal journal)
        {
            _context.Journal.Add(journal);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (JournalExists(journal.Index))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetJournal", new { id = journal.Index }, journal);
        }

        // DELETE: api/Journals/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Journal>> DeleteJournal(decimal id)
        {
            var journal = await _context.Journal.FindAsync(id);
            if (journal == null)
            {
                return NotFound();
            }

            _context.Journal.Remove(journal);
            await _context.SaveChangesAsync();

            return journal;
        }

        private bool JournalExists(decimal id)
        {
            return _context.Journal.Any(e => e.Index == id);
        }
    }
}
