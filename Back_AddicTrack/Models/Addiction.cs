using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using Back_AddicTrack.Models.DTOs;

namespace Back_AddicTrack.Models;

public class Addiction
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Key]
    public Guid Id { get; set; }

    [Required] [StringLength(50)] public string Name { get; set; }

    [Required] public DateTime SobrietyStartDateTime { get; set; }

    [Required] public HealthProfessional HealthProfessional { get; set; }

    public void UpdateFromDTO(AddictionDTO addictionDTO, HealthProfessional healthProfessional)
    {
        if (!DateTime.TryParseExact(addictionDTO.SobrietyStartDateTime, "O", CultureInfo.InvariantCulture,
                DateTimeStyles.None, out var sobrietyStartDateTime))
            throw new BadHttpRequestException("Invalid datetime : " + addictionDTO.SobrietyStartDateTime);

        Name = addictionDTO.Name;
        SobrietyStartDateTime = sobrietyStartDateTime;
        HealthProfessional = healthProfessional;
    }
}