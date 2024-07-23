namespace Movilissa.core.DTOs.Ticket;

public class TicketAvailabilityData
{
    public int OriginId { get; set; }
    public int DestinyId { get; set; }
    public DateTime Date { get; set; }
    public int? CompanyId { get; set; }
}