using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Back_AddicTrack.Models.DTOs;

namespace Back_AddicTrack.Models;

public class Addiction
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Key]
    public Guid Id { get; set; }

    [Required] [StringLength(50)] public string Name { get; set; }

    [Required]
    [DataType(DataType.DateTime)]
    public DateTime SobrietyStartDateTime { get; set; }

    [Required] public HealthProfessional HealthProfessional { get; set; }

    public void UpdateFromDTO(AddictionDTO addictionDTO, HealthProfessional healthProfessional)
    {
        Name = addictionDTO.Name;
        SobrietyStartDateTime = addictionDTO.SobrietyStartDateTime;
        HealthProfessional = healthProfessional;
    }
}