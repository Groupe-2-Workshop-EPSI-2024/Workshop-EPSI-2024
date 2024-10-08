using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Back_AddicTrack.Data;
using Back_AddicTrack.Models;

namespace Back_AddicTrack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HealthProfessionalsController : ControllerBase
    {
        private readonly DataContext _context;

        public HealthProfessionalsController(DataContext context)
        {
            _context = context;
        }

        // GET: api/HealthProfessionals
        [HttpGet]
        public async Task<ActionResult<IEnumerable<HealthProfessional>>> GetHealthProfessionals()
        {
            return await _context.HealthProfessionals.ToListAsync();
        }

        // GET: api/HealthProfessionals/5
        [HttpGet("{id}")]
        public async Task<ActionResult<HealthProfessional>> GetHealthProfessional(Guid id)
        {
            var healthProfessional = await _context.HealthProfessionals.FindAsync(id);

            if (healthProfessional == null)
            {
                return NotFound();
            }

            return healthProfessional;
        }

        // PUT: api/HealthProfessionals/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHealthProfessional(Guid id, HealthProfessional healthProfessional)
        {
            if (id != healthProfessional.Id)
            {
                return BadRequest();
            }

            _context.Entry(healthProfessional).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HealthProfessionalExists(id))
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

        // POST: api/HealthProfessionals
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<HealthProfessional>> PostHealthProfessional(HealthProfessional healthProfessional)
        {
            _context.HealthProfessionals.Add(healthProfessional);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetHealthProfessional), new { id = healthProfessional.Id }, healthProfessional);
        }

        // DELETE: api/HealthProfessionals/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHealthProfessional(Guid id)
        {
            var healthProfessional = await _context.HealthProfessionals.FindAsync(id);
            if (healthProfessional == null)
            {
                return NotFound();
            }

            _context.HealthProfessionals.Remove(healthProfessional);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool HealthProfessionalExists(Guid id)
        {
            return _context.HealthProfessionals.Any(e => e.Id == id);
        }
    }
}
