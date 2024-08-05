using System.Text.Json.Serialization;
using Movilissa.core.DTOs.Shared;

namespace Movilissa.core.DTOs.Bus;

public class BusDetailed
{
    [JsonPropertyName("id")]
    public int Id { get; set; }
    
    [JsonPropertyName("identification_number")]
    public string IdentificationNumber { get; set; }
    
    [JsonPropertyName("license_plate")]
    public string LicensePlate { get; set; }
    
    [JsonPropertyName("company_name")]
    public string CompanyName { get; set; }
    
    [JsonPropertyName("model")]
    public string Model { get; set; }
    
    [JsonPropertyName("brand")]
    public string Brand { get; set; }
    
    [JsonPropertyName("seating_capacity")]
    public int SeatingCapacity { get; set; }
    
    [JsonPropertyName("status")]
    public Item Status { get; set; }
    
    [JsonPropertyName("amenities")]
    public List<Item> Amenities { get; set; }
}