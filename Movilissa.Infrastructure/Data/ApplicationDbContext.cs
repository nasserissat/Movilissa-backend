using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Movilissa_api.Models;
using Route = Movilissa_api.Models.Route;
namespace Movilissa_api.Data.Context;

public class ApplicationDbContext : IdentityDbContext<User>
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

    protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(u => u.CompanyId).IsRequired(false);

                // Configurar la relación uno a muchos con Company
                entity.HasOne(u => u.Company)
                    .WithMany(c => c.Users)
                    .HasForeignKey(u => u.CompanyId)
                    .OnDelete(DeleteBehavior.SetNull); 
            });


            // Configuración de Ticket
            modelBuilder.Entity<Ticket>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.PurchaseDate)
                    .IsRequired();

                entity.HasOne(e => e.User)
                    .WithMany(u => u.Tickets)
                    .HasForeignKey(e => e.Id)
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