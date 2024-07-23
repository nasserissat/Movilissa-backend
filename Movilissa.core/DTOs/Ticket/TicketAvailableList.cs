using Movilissa.core.DTOs.Shared;

namespace Movilissa.core.DTOs.Ticket;

public class TicketAvailableList
{
    public int Id { get; set; }
    public Item Company { get; set; }
    public Item Origin { get; set; }
    public Item Destiny { get; set; }
    public DateTime Date { get; set; }
    public string DepartureTime { get; set; }
    public string ArrivalTime { get; set; }
    public string EstimatedDuration { get; set; }
    public decimal Price { get; set; }
    public int? SeatsAvailable { get; set; }
}