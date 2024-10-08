using Back_AddicTrack.Models;
using Microsoft.EntityFrameworkCore;

namespace Back_AddicTrack.Data;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options)
        : base(options)
    {
    }

    public DbSet<User> Users { get; set; } = null!;
    public DbSet<Patient> Patients { get; set; } = null!;
    public DbSet<HealthProfessional> HealthProfessionals { get; set; } = null!;
    public DbSet<Addiction> Addictions { get; set; } = null!;
}