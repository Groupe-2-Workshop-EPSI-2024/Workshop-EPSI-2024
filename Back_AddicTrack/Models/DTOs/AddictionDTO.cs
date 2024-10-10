using System.ComponentModel.DataAnnotations;

namespace Back_AddicTrack.Models.DTOs;

public class AddictionDTO
{
    public Guid Id { get; set; }

    [StringLength(50)] public string Name { get; set; }

    [DataType(DataType.DateTime)] public DateTime SobrietyStartDateTime { get; set; }

    public Guid HealthProfessionalId { get; set; }

    public static AddictionDTO FromAddiction(Addiction addiction)
    {
        return new AddictionDTO
        {
            Id = addiction.Id,
            Name = addiction.Name,
            SobrietyStartDateTime = addiction.SobrietyStartDateTime,
            HealthProfessionalId = addiction.HealthProfessional.Id
        };
    }
}