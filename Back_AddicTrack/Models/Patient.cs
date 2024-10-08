namespace Back_AddicTrack.Models;

public class Patient : User
{
    public ICollection<Addiction> Addictions { get; set; }
}