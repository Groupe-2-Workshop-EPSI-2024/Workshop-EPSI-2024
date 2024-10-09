using System.Globalization;

namespace Back_AddicTrack.Models.DTOs;

public class AddictionDTO
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public string SobrietyStartDateTime { get; set; }

    public Guid HealthProfessionalId { get; set; }

    public static AddictionDTO FromAddiction(Addiction addiction)
    {
        return new AddictionDTO
        {
            Id = addiction.Id,
            Name = addiction.Name,
            SobrietyStartDateTime = addiction.SobrietyStartDateTime.ToString("O", CultureInfo.InvariantCulture),
            HealthProfessionalId = addiction.HealthProfessional.Id
        };
    }
}