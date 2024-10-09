using System.ComponentModel.DataAnnotations;
using System.Globalization;
using Back_AddicTrack.Models.DTOs;
using Back_AddicTrack.Models.Enums;

namespace Back_AddicTrack.Models;

public class Patient : User
{
    [Required] [DataType(DataType.Date)] public DateTime BirthDate { get; set; }

    [Required] public Gender Gender { get; set; }

    public ICollection<Addiction> Addictions { get; set; } = new HashSet<Addiction>();

    public void UpdateFromDTO(PatientDTO patientDTO)
    {
        if (!DateOnly.TryParseExact(patientDTO.BirthDate, "O", CultureInfo.InvariantCulture, DateTimeStyles.None,
                out var birthDate))
            throw new BadHttpRequestException("Invalid date : " + patientDTO.BirthDate);

        if (!Enum.TryParse(patientDTO.Gender, out Gender gender))
            throw new BadHttpRequestException("Invalid gender : " + patientDTO.Gender);

        FirstName = patientDTO.FirstName;
        LastName = patientDTO.LastName;
        Email = patientDTO.Email;
        PhoneNumber = patientDTO.PhoneNumber;
        BirthDate = birthDate.ToDateTime(TimeOnly.MinValue);
        Gender = gender;
    }
}