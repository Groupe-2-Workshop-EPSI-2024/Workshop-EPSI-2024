using Back_AddicTrack.Data;
using Back_AddicTrack.Models;
using Back_AddicTrack.Models.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Back_AddicTrack.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PatientsController : ControllerBase
{
    private readonly DataContext _context;

    public PatientsController(DataContext context)
    {
        _context = context;
    }

    // GET: api/Patients
    [HttpGet]
    public async Task<ActionResult<IEnumerable<PatientDTO>>> GetPatients()
    {
        return await _context.Patients
            .Select(e => PatientToDTO(e))
            .ToListAsync();
    }

    // GET: api/Patients/5
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<PatientDTO>> GetPatient(Guid id)
    {
        var patient = await _context.Patients.FindAsync(id);

        if (patient == null) return NotFound();

        return PatientToDTO(patient);
    }

    // PUT: api/Patients/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> PutPatient(Guid id, PatientDTO patientDTO)
    {
        if (id != patientDTO.Id) return BadRequest();

        var patient = await _context.Patients.FindAsync(id);
        if (patient == null) return NotFound();

        patient.FirstName = patientDTO.FirstName;
        patient.LastName = patientDTO.LastName;

        try
        {
            await _context.SaveChangesAsync();
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
        var patient = new Patient
        {
            FirstName = patientDTO.FirstName,
            LastName = patientDTO.LastName
        };

        _context.Patients.Add(patient);
        await _context.SaveChangesAsync();

        return CreatedAtAction(
            nameof(GetPatient),
            new { id = patient.Id },
            PatientToDTO(patient));
    }

    // DELETE: api/Patients/5
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeletePatient(Guid id)
    {
        var patient = await _context.Patients.FindAsync(id);
        if (patient == null) return NotFound();

        _context.Patients.Remove(patient);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool PatientExists(Guid id)
    {
        return _context.Patients.Any(e => e.Id == id);
    }

    private static PatientDTO PatientToDTO(Patient patient)
    {
        return new PatientDTO
        {
            Id = patient.Id,
            FirstName = patient.FirstName,
            LastName = patient.LastName
        };
    }
}