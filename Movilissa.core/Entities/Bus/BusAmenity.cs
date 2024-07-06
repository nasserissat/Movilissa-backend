namespace Movilissa_api.Models;

public class BusAmenity
{
    public int Id { get; set; }
    
    public int BusId { get; set; }
    public Bus Bus { get; set; }

    public int AmenityId { get; set; }
    public Amenity Amenity { get; set; }
}