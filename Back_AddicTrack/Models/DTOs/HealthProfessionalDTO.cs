namespace Back_AddicTrack.Models.DTOs;

public class HealthProfessionalDTO : UserDTO
{
    public static HealthProfessionalDTO FromHealthProfessional(HealthProfessional healthProfessional)
    {
        return new HealthProfessionalDTO
        {
            Id = healthProfessional.Id,
            FirstName = healthProfessional.FirstName,
            LastName = healthProfessional.LastName,
            Email = healthProfessional.Email,
            PhoneNumber = healthProfessional.PhoneNumber
        };
    }
}