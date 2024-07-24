using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Movilissa_api.Models;
using Route = Movilissa_api.Models.Route;
namespace Movilissa_api.Data.Context;

public class ApplicationDbContext: IdentityDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<TicketStatus> TicketStatuses { get; set; }
    public DbSet<Ticket> Tickets { get; set; }
    public DbSet<Schedule> Schedules { get; set; }
    public DbSet<BusSchedule> BusSchedules { get; set; }
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
    public DbSet<BusType> BusTypes { get; set; }
    public DbSet<RouteDestination> RouteDestinations { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Configuración de User
            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.PasswordHash)
                    .IsRequired();

                entity.HasIndex(e => e.Email)
                    .IsUnique();
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
            
            modelBuilder.Entity<BusAmenity>()
                .HasKey(ba => new { ba.BusId, ba.AmenityId });

            modelBuilder.Entity<BusAmenity>()
                .HasOne(ba => ba.Bus)
                .WithMany(b => b.Amenities)
                .HasForeignKey(ba => ba.BusId);

            modelBuilder.Entity<BusAmenity>()
                .HasOne(ba => ba.Amenity)
                .WithMany(a => a.Buses)
                .HasForeignKey(ba => ba.AmenityId);
            // Relación entre Route y RouteDestination
            
            modelBuilder.Entity<RouteDestination>()
                .HasOne(rd => rd.Route)
                .WithMany(r => r.Destinations)
                .HasForeignKey(rd => rd.RouteId);

            modelBuilder.Entity<RouteDestination>()
                .HasOne(rd => rd.Destination)
                .WithMany()
                .HasForeignKey(rd => rd.DestinationId);

        }
    
}