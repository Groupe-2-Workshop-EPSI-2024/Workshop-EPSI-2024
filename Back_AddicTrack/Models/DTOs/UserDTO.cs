using System.ComponentModel.DataAnnotations;

namespace Back_AddicTrack.Models.DTOs;

public abstract class UserDTO
{
    public Guid Id { get; set; }


    [StringLength(50)] public string FirstName { get; set; }

    [StringLength(50)] public string LastName { get; set; }

    [StringLength(320)]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; }

    [StringLength(15)]
    [DataType(DataType.PhoneNumber)]
    public string PhoneNumber { get; set; }
}