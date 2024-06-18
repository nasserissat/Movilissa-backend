using Microsoft.EntityFrameworkCore;
using Movilissa_api.Models;
using Route = Microsoft.AspNetCore.Routing.Route;

namespace Movilissa_api.Data.Context;

public class ApplicationDbContext: DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<TicketStatus> TicketStatuses { get; set; }
    public DbSet<Ticket> Tickets { get; set; }
    public DbSet<Schedule> Schedules { get; set; }
    public DbSet<Route> Routes { get; set; }
    public DbSet<Province> Provinces { get; set; }
    public DbSet<Payment> Payments { get; set; }
    public DbSet<InvoiceStatus> InvoiceStatuses { get; set; }
    public DbSet<InvoiceDetail> InvoiceDetails { get; set; }
    public DbSet<Invoice> Invoices { get; set; }
    public DbSet<Country> Countries { get; set; }
    public DbSet<Company> Companies { get; set; }
    public DbSet<Bus> Buses { get; set; }
    public DbSet<Branch> Branches { get; set; }
    public DbSet<Amenity> Amenities { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuración de User
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.UserId);
                
                entity.Property(e => e.PasswordHash)
                    .IsRequired();

                entity.HasIndex(e => e.Email)
                    .IsUnique();

                // Relaciones
                entity.HasOne(e => e.Province)
                    .WithMany()
                    .HasForeignKey(e => e.ProvinceId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.Country)
                    .WithMany()
                    .HasForeignKey(e => e.CountryId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // Configuración de Ticket
            modelBuilder.Entity<Ticket>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.PurchaseDate)
                    .IsRequired();

                entity.HasOne(e => e.User)
                    .WithMany(u => u.Tickets)
                    .HasForeignKey(e => e.UserId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(e => e.Status)
                    .WithMany()
                    .HasForeignKey(e => e.StatusId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.Schedule)
                    .WithMany()
                    .HasForeignKey(e => e.ScheduleId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

        }
    
}