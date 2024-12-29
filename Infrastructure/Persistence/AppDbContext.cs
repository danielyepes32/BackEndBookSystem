using Backend_agendamientos.Core.Entities;
using Microsoft.EntityFrameworkCore;
public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Reservation> Reservations { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Space> Spaces { get; set; }
}