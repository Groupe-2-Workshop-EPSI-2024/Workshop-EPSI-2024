using Back_AddicTrack.Data;
using Back_AddicTrack.Models;
using Back_AddicTrack.Models.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Back_AddicTrack.Controllers;

[Route("api/Patients/{patientId:guid}/[controller]")]
[ApiController]
public class AddictionsController(DataContext context) : ControllerBase
{
    // GET: api/Patients/5/Addictions
    [HttpGet]
    public async Task<ActionResult<IEnumerable<AddictionDTO>>> GetAddictions(Guid patientId, Guid? healthProfessionalId)
    {
        if (!context.Patients.Any(p => p.Id == patientId)) return NotFound();
        if (healthProfessionalId != null && !context.HealthProfessionals.Any(h => h.Id == healthProfessionalId))
            return NotFound();

        return await context.Patients
            .Where(p => p.Id == patientId)
            .SelectMany(p => p.Addictions)
            .Where(a =>
                healthProfessionalId == null
                || a.HealthProfessional.Id == healthProfessionalId)
            .Include(a => a.HealthProfessional)
            .Select(a => AddictionDTO.FromAddiction(a))
            .ToListAsync();
    }

    // GET: api/Patients/5/Addictions/3
    [HttpGet("{addictionId:guid}")]
    public async Task<ActionResult<AddictionDTO>> GetAddiction(Guid patientId, Guid addictionId)
    {
        var addiction = await context.Patients
            .Where(p => p.Id == patientId)
            .SelectMany(p => p.Addictions)
            .Where(a => a.Id == addictionId)
            .Include(a => a.HealthProfessional)
            .FirstOrDefaultAsync();
        if (addiction == null) return NotFound();

        return AddictionDTO.FromAddiction(addiction);
    }

    // PUT: api/Patients/5/Addictions/3
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{addictionId:guid}")]
    public async Task<IActionResult> PutAddiction(Guid patientId, Guid addictionId, AddictionDTO addictionDTO)
    {
        if (addictionId != addictionDTO.Id) return BadRequest();

        var addiction = await context.Patients
            .Where(p => p.Id == patientId)
            .SelectMany(p => p.Addictions)
            .Where(a => a.Id == addictionId)
            .FirstOrDefaultAsync();
        if (addiction == null) return NotFound();

        var healthProfessional = await context.HealthProfessionals.FindAsync(addictionDTO.HealthProfessionalId);
        if (healthProfessional == null) return NotFound();

        try
        {
            addiction.UpdateFromDTO(addictionDTO, healthProfessional);
        }
        catch (BadHttpRequestException e)
        {
            return BadRequest(e.Message);
        }

        try
        {
            await context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException) when (!AddictionExists(patientId, addictionId))
        {
            return NotFound();
        }

        return NoContent();
    }

    // POST: api/Patients/5/Addictions
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<PatientDTO>> PostAddiction(Guid patientId, AddictionDTO addictionDTO)
    {
        var patient = await context.Patients.FindAsync(patientId);
        if (patient == null) return NotFound();

        var healthProfessional = await context.HealthProfessionals.FindAsync(addictionDTO.HealthProfessionalId);
        if (healthProfessional == null) return NotFound();

        var addiction = new Addiction();

        try
        {
            addiction.UpdateFromDTO(addictionDTO, healthProfessional);
        }
        catch (BadHttpRequestException e)
        {
            return BadRequest(e.Message);
        }

        context.Addictions.Add(addiction);
        patient.Addictions.Add(addiction);
        await context.SaveChangesAsync();

        return CreatedAtAction(
            nameof(GetAddiction),
            new { patientId = patient.Id, addictionId = addiction.Id },
            AddictionDTO.FromAddiction(addiction));
    }

    // DELETE: api/Patients/5/Addictions/3
    [HttpDelete("{addictionId:guid}")]
    public async Task<IActionResult> DeleteAddiction(Guid patientId, Guid addictionId)
    {
        var patient = await context.Patients.FindAsync(patientId);
        if (patient == null) return NotFound();

        var addiction = await context.Patients
            .Where(p => p.Id == patientId)
            .SelectMany(p => p.Addictions)
            .Where(a => a.Id == addictionId)
            .FirstOrDefaultAsync();
        if (addiction == null) return NotFound();

        patient.Addictions.Remove(addiction);
        await context.SaveChangesAsync();

        return NoContent();
    }

    private bool AddictionExists(Guid patientId, Guid addictionId)
    {
        return context.Patients.Any(p =>
            p.Id == patientId
            && p.Addictions.Any(a =>
                a.Id == addictionId));
    }
}