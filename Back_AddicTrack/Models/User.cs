using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Back_AddicTrack.Models;

public abstract class User
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Key]
    public Guid Id { get; set; }

    [Required] [StringLength(50)] public string FirstName { get; set; }

    [Required] [StringLength(50)] public string LastName { get; set; }

    [Required]
    [StringLength(320)]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; }

    [Required]
    [StringLength(15)]
    [DataType(DataType.PhoneNumber)]
    public string PhoneNumber { get; set; }
}