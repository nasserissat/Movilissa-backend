namespace Movilissa_api.Models;

public class RouteDestination
{
    public int Id { get; set; }
    public int RouteId { get; set; }
    public int DestinationId { get; set; }
    public decimal Price { get; set; }
    public Route Route { get; set; }
    public Branch Destination { get; set; }
}