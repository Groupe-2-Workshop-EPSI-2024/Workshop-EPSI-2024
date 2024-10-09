namespace Back_AddicTrack.Models.DTOs;

public abstract class UserDTO
{
    public Guid Id { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string Email { get; set; }

    public string PhoneNumber { get; set; }
}