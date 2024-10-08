using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Back_AddicTrack.Models;

public class Addiction
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Key]
    public Guid Id { get; set; }

    [Required] [StringLength(50)] public string Name { get; set; }

    [Required] public DateTime SobrietyStartDateTime { get; set; }
}