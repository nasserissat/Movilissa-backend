namespace Movilissa_api.Models;

public class Route
{
    public int Id { get; set; }
    public int OriginId { get; set; }
    public int DestinationId { get; set; }
    
    public Branch Origin { get; set; }
    public Branch Destination { get; set; }

    public ICollection<Schedule> Schedules { get; set; } // Navigation property

    
}