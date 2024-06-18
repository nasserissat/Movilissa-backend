namespace Movilissa_api.Models;

public class Travel
{
    public int Id { get; set; }
    public int OriginId { get; set; }
    public int DestinationId { get; set; }
    public DateTime DepartureTime { get; set; }
    public DateTime ArrivalTime { get; set; }
    public DateTime Date { get; set; }
    public decimal Price { get; set; }
    
    public Branch Origin { get; set; }
    public Branch Destination { get; set; }
    public ICollection<Ticket>? Tickets { get; set; }
}
