namespace Movilissa_api.Models;

public class Route
{
    public int Id { get; set; }
    public int OriginId { get; set; }
    public Branch Origin { get; set; }
    public int CompanyId { get; set; }
    public virtual Company Company { get; set; }
    public ICollection<RouteDestination> Destinations { get; set; } // Relación a muchos destinos

    public ICollection<Schedule> Schedules { get; set; }
    
}