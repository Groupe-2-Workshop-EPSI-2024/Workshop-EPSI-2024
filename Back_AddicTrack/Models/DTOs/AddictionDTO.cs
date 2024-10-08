namespace Back_AddicTrack.Models.DTOs;

public class AddictionDTO
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public DateTime SobrietyStartDateTime { get; set; }
}