namespace Movilissa_api.Models;

public class Amenity
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Status { get; set; }

    public int CompanyId { get; set; }
    public virtual Company Company { get; set; }
    public ICollection<BusAmenity> Buses { get; set; } 
}