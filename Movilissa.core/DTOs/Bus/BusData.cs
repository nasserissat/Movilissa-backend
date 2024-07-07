namespace Movilissa.core.DTOs.Bus;

public class BusData
{
    public string IdentificationNumber { get; set; }
    public string LicensePlate { get; set; }
    public int CompanyId { get; set; }
    public int BusTypeId { get; set; }
    
    public List<int> AmenityIds { get; set; } 

    
}