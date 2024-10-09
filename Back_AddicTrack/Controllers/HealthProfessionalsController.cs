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
            .Select(h => HealthProfessionalDTO.FromHealthProfessional(h))
            .ToListAsync();
    }

    // GET: api/HealthProfessionals/5
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<HealthProfessionalDTO>> GetHealthProfessional(Guid id)
    {
        var healthProfessional = await context.HealthProfessionals.FindAsync(id);

        if (healthProfessional == null) return NotFound();

        return HealthProfessionalDTO.FromHealthProfessional(healthProfessional);
    }

    // GET: api/HealthProfessionals/5/Patients
    [HttpGet("{id:guid}/Patients")]
    public async Task<ActionResult<IEnumerable<PatientDTO>>> GetHealthProfessionalPatients(Guid id)
    {
        if (!HealthProfessionalExists(id)) return NotFound();

        return await context.Patients
            .Where(p =>
                p.Addictions.Any(a =>
                    a.HealthProfessional.Id == id))
            .Select(p => PatientDTO.FromPatient(p))
            .ToListAsync();
    }

    // PUT: api/HealthProfessionals/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> PutHealthProfessional(Guid id, HealthProfessionalDTO healthProfessionalDTO)
    {
        if (id != healthProfessionalDTO.Id) return BadRequest();

        var healthProfessional = await context.HealthProfessionals.FindAsync(id);
        if (healthProfessional == null) return NotFound();

        healthProfessional.UpdateFromDTO(healthProfessionalDTO);

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
        var healthProfessional = new HealthProfessional();

        healthProfessional.UpdateFromDTO(healthProfessionalDTO);

        context.HealthProfessionals.Add(healthProfessional);
        await context.SaveChangesAsync();

        return CreatedAtAction(
            nameof(GetHealthProfessional),
            new { id = healthProfessional.Id },
            HealthProfessionalDTO.FromHealthProfessional(healthProfessional));
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
        return context.HealthProfessionals.Any(h => h.Id == id);
    }
}