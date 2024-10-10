using Back_AddicTrack.Data;
using Back_AddicTrack.Models;
using Back_AddicTrack.Models.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Back_AddicTrack.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PatientsController(DataContext context) : ControllerBase
{
    // GET: api/Patients
    [HttpGet]
    public async Task<ActionResult<IEnumerable<PatientDTO>>> GetPatients()
    {
        return await context.Patients
            .Select(p => PatientDTO.FromPatient(p))
            .ToListAsync();
    }

    // GET: api/Patients/5
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<PatientDTO>> GetPatient(Guid id)
    {
        var patient = await context.Patients.FindAsync(id);

        if (patient == null) return NotFound();

        return PatientDTO.FromPatient(patient);
    }

    // PUT: api/Patients/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> PutPatient(Guid id, PatientDTO patientDTO)
    {
        if (id != patientDTO.Id) return BadRequest();

        var patient = await context.Patients.FindAsync(id);
        if (patient == null) return NotFound();

        patient.UpdateFromDTO(patientDTO);

        try
        {
            await context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException) when (!PatientExists(id))
        {
            return NotFound();
        }

        return NoContent();
    }

    // POST: api/Patients
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<PatientDTO>> PostPatient(PatientDTO patientDTO)
    {
        var patient = new Patient();

        patient.UpdateFromDTO(patientDTO);

        context.Patients.Add(patient);
        await context.SaveChangesAsync();

        return CreatedAtAction(
            nameof(GetPatient),
            new { id = patient.Id },
            PatientDTO.FromPatient(patient));
    }

    // DELETE: api/Patients/5
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeletePatient(Guid id)
    {
        var patient = await context.Patients.FindAsync(id);
        if (patient == null) return NotFound();

        context.Patients.Remove(patient);
        await context.SaveChangesAsync();

        return NoContent();
    }

    private bool PatientExists(Guid id)
    {
        return context.Patients.Any(p => p.Id == id);
    }
}