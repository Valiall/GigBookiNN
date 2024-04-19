using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using GigBookin.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace GigBookin.Data
{
  public class ApplicationDbContext : IdentityDbContext<EventOrganiser, IdentityRole<Guid>, Guid>
    {
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
    public DbSet<Event> Events { get; set; }
    public DbSet<Genre> Genres { get; set; }
    public DbSet<Performer> Performers { get; set; }
    protected override void OnModelCreating(ModelBuilder builder)
    {

      base.OnModelCreating(builder);
    
     
      builder.Entity<EventPerformer>()
      .HasKey(x => new { x.PerformerId, x.EventOrganiserId });

      builder.Entity<EventOrganiser>()
          .Property(u => u.Name)
          .HasMaxLength(50)
          .IsRequired(false);

      builder.Entity<EventOrganiser>()
        .Property(u => u.Description)
        .HasMaxLength(300)
        .IsRequired(false);

      builder.Entity<EventOrganiser>()
        .Property(u => u.Experience)
        .HasMaxLength(100)
        .IsRequired(false);

      builder.Entity<EventOrganiser>()
        .Property(u => u.FirmName)
        .HasMaxLength(30)
        .IsRequired(false);

      builder.Entity<EventOrganiser>()
        .Property(u => u.FirmLocation)
        .HasMaxLength(30)
        .IsRequired(false);

      builder.Entity<EventOrganiser>()
        .Property(eo => eo.Balance)
        .HasColumnType("decimal(18, 2)"); // Adjust precision and scale as needed

      builder.Entity<Performer>()
        .Property(p => p.Price)
        .HasColumnType("decimal(18, 2)"); 
    }
  }
}