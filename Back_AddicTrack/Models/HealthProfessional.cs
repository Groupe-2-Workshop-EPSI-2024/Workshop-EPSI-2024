using Back_AddicTrack.Models.DTOs;

namespace Back_AddicTrack.Models;

public class HealthProfessional : User
{
    public void UpdateFromDTO(HealthProfessionalDTO healthProfessionalDTO)
    {
        FirstName = healthProfessionalDTO.FirstName;
        LastName = healthProfessionalDTO.LastName;
        Email = healthProfessionalDTO.Email;
        PhoneNumber = healthProfessionalDTO.PhoneNumber;
    }
}