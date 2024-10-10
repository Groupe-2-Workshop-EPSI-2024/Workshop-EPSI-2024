using System.ComponentModel.DataAnnotations;
using Back_AddicTrack.Models.Enums;

namespace Back_AddicTrack.Models.DTOs;

public class PatientDTO : UserDTO
{
    [DataType(DataType.Date)] public DateTime BirthDate { get; set; }

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
            BirthDate = patient.BirthDate,
            Gender = patient.Gender
        };
    }
}