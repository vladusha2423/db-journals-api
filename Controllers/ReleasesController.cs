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
    public class ReleasesController : ControllerBase
    {
        private readonly labsContext _context;

        public ReleasesController(labsContext context)
        {
            _context = context;
        }

        // GET: api/Releases
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Release>>> GetRelease()
        {
            return await _context.Release.ToListAsync();
        }

        // GET: api/Releases/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Release>> GetRelease(decimal id)
        {
            var release = await _context.Release.FindAsync(id);

            if (release == null)
            {
                return NotFound();
            }

            return release;
        }

        // PUT: api/Releases/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRelease(decimal id, Release release)
        {
            if (id != release.Id)
            {
                return BadRequest();
            }

            _context.Entry(release).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReleaseExists(id))
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

        // POST: api/Releases
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Release>> PostRelease(Release release)
        {
            _context.Release.Add(release);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ReleaseExists(release.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetRelease", new { id = release.Id }, release);
        }

        // DELETE: api/Releases/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Release>> DeleteRelease(decimal id)
        {
            var release = await _context.Release.FindAsync(id);
            if (release == null)
            {
                return NotFound();
            }

            _context.Release.Remove(release);
            await _context.SaveChangesAsync();

            return release;
        }

        private bool ReleaseExists(decimal id)
        {
            return _context.Release.Any(e => e.Id == id);
        }
    }
}
