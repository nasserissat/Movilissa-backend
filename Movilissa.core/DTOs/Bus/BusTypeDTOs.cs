using System.Text.Json.Serialization;
using Movilissa.core.DTOs.Shared;

namespace Movilissa.core.DTOs.Bus.AmenityDTOs;

public class BusTypeData
{
    [JsonPropertyName("brand_id")]
    public int BrandId { get; set; }

    [JsonPropertyName("model")]
    public string Model { get; set; }

    [JsonPropertyName("capacity")]
    public int Capacity { get; set; }

    [JsonPropertyName("status_id")]
    public int StatusId { get; set; }
    
    [JsonPropertyName("company_id")]
    public int CompanyId { get; set; }
}

public class BusTypeList
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("brand")]
    public Item Brand { get; set; }

    [JsonPropertyName("model")]
    public string Model { get; set; }

    [JsonPropertyName("capacity")]
    public int Capacity { get; set; }
    
    [JsonPropertyName("status")]
    public Item Status { get; set; }

}
public class BusTypeFilter
{
    public string? Name { get; set; }
    public int? StatusId { get; set; }
}
