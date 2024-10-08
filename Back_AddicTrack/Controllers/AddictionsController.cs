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
    public async Task<ActionResult<IEnumerable<AddictionDTO>>> GetAddictions(Guid patientId)
    {
        if (!context.Patients.Any(e => e.Id == patientId)) return NotFound();

        return await context.Patients
            .Where(p => p.Id == patientId)
            .SelectMany(p => p.Addictions)
            .Select(a => AddictionToDTO(a))
            .ToListAsync();
    }

    private static AddictionDTO AddictionToDTO(Addiction addiction)
    {
        return new AddictionDTO
        {
            Id = addiction.Id,
            Name = addiction.Name,
            SobrietyStartDateTime = addiction.SobrietyStartDateTime
        };
    }
}