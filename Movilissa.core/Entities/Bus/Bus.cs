namespace Movilissa_api.Models;

public class Bus
{
    public int Id { get; set; }
    public string IdentificationNumber { get; set; }
    public string LicensePlate { get; set; }
    public int CompanyId { get; set; }
    public int BusTypeId { get; set; }
    
    public int? StatusId { get; set; }


    public virtual Company Company { get; set; }
    public ICollection<BusAmenity> Amenities { get; set; }
    public ICollection<BusSchedule> BusSchedules { get; set; }
    public virtual BusType BusType { get; set; }
    public BusStatus Status { get; set; } 


}