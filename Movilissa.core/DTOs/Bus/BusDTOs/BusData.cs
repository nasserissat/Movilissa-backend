using System.Text.Json.Serialization;
using Movilissa.core.DTOs.Shared;

namespace Movilissa.core.DTOs.Bus;

public class BusData
{
    [JsonPropertyName("identification_number")]
    public string IdentificationNumber { get; set; }
    
    [JsonPropertyName("license_plate")]
    public string LicensePlate { get; set; }
    
    [JsonPropertyName("company_id")]
    public int CompanyId { get; set; }
    
    [JsonPropertyName("bus_type_id")]
    public int BusTypeId { get; set; }
    
    [JsonPropertyName("status_id")]
    public int StatusId { get; set; }
    
    [JsonPropertyName("amenity_ids")]
    public List<int> AmenityIds { get; set; }
}