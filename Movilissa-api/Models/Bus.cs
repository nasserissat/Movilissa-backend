namespace Movilissa_api.Models;

public class Bus
{
    public int Id { get; set; }
    public string Brand { get; set; }
    public string Model { get; set; }
    public string IdentificationNumber { get; set; }
    public int SeatingCapacity { get; set; }
    public int CompanyId { get; set; }
    
    public virtual Company Company { get; set; }
    public ICollection<Amenity> Amenities { get; set; }

}