namespace Movilissa_api.Models;

public class Ticket
{
    public int Id { get; set; }
    public DateTime PurchaseDate { get; set; }
    public int UserId { get; set; }
    public int ScheduleId { get; set; }
    public string? SeatNumber { get; set; }
    public string QRCode { get; set; }
    public int StatusId { get; private set; }
    public int CompanyId { get; set; }

    public User User { get; set; }
    public TicketStatus Status { get; set; }

    public Schedule Schedule { get; set; }
    public virtual Company Company { get; set; }
}