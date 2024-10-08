using Back_AddicTrack.Data;
using Back_AddicTrack.Models;
using Back_AddicTrack.Models.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Back_AddicTrack.Controllers;

[Route("api/[controller]")]
[ApiController]
public class HealthProfessionalsController(DataContext context) : ControllerBase
{
    // GET: api/HealthProfessionals
    [HttpGet]
    public async Task<ActionResult<IEnumerable<HealthProfessionalDTO>>> GetHealthProfessionals()
    {
        return await context.HealthProfessionals
            .Select(e => HealthProfessionalToDTO(e))
            .ToListAsync();
    }

    // GET: api/HealthProfessionals/5
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<HealthProfessionalDTO>> GetHealthProfessional(Guid id)
    {
        var healthProfessional = await context.HealthProfessionals.FindAsync(id);

        if (healthProfessional == null) return NotFound();

        return HealthProfessionalToDTO(healthProfessional);
    }

    // PUT: api/HealthProfessionals/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> PutHealthProfessional(Guid id, HealthProfessionalDTO healthProfessionalDTO)
    {
        if (id != healthProfessionalDTO.Id) return BadRequest();

        var healthProfessional = await context.HealthProfessionals.FindAsync(id);
        if (healthProfessional == null) return NotFound();

        healthProfessional.FirstName = healthProfessionalDTO.FirstName;
        healthProfessional.LastName = healthProfessionalDTO.LastName;

        try
        {
            await context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException) when (!HealthProfessionalExists(id))
        {
            return NotFound();
        }

        return NoContent();
    }

    // POST: api/HealthProfessionals
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<HealthProfessionalDTO>> PostHealthProfessional(
        HealthProfessionalDTO healthProfessionalDTO)
    {
        var healthProfessional = new HealthProfessional
        {
            FirstName = healthProfessionalDTO.FirstName,
            LastName = healthProfessionalDTO.LastName
        };

        context.HealthProfessionals.Add(healthProfessional);
        await context.SaveChangesAsync();

        return CreatedAtAction(
            nameof(GetHealthProfessional),
            new { id = healthProfessional.Id },
            HealthProfessionalToDTO(healthProfessional));
    }

    // DELETE: api/HealthProfessionals/5
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteHealthProfessional(Guid id)
    {
        var healthProfessional = await context.HealthProfessionals.FindAsync(id);
        if (healthProfessional == null) return NotFound();

        context.HealthProfessionals.Remove(healthProfessional);
        await context.SaveChangesAsync();

        return NoContent();
    }

    private bool HealthProfessionalExists(Guid id)
    {
        return context.HealthProfessionals.Any(e => e.Id == id);
    }

    private static HealthProfessionalDTO HealthProfessionalToDTO(HealthProfessional healthProfessional)
    {
        return new HealthProfessionalDTO
        {
            Id = healthProfessional.Id,
            FirstName = healthProfessional.FirstName,
            LastName = healthProfessional.LastName
        };
    }
}