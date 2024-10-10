using System.Globalization;
using Back_AddicTrack.Models.Enums;

namespace Back_AddicTrack.Models.DTOs;

public class PatientDTO : UserDTO
{
    public string BirthDate { get; set; }

    public Gender Gender { get; set; }

    public static PatientDTO FromPatient(Patient patient)
    {
        return new PatientDTO
        {
            Id = patient.Id,
            FirstName = patient.FirstName,
            LastName = patient.LastName,
            Email = patient.Email,
            PhoneNumber = patient.PhoneNumber,
            BirthDate = DateOnly.FromDateTime(patient.BirthDate).ToString("O", CultureInfo.InvariantCulture),
            Gender = patient.Gender
        };
    }
}