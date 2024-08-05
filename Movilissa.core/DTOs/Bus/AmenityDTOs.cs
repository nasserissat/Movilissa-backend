using System.Text.Json.Serialization;
using Movilissa.core.DTOs.Shared;

namespace Movilissa.core.DTOs.Bus.AmenityDTOs;

public class AmenityData
{
    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("status_id")]
    public int? StatusId { get; set; }

    [JsonPropertyName("company_id")]
    public int CompanyId { get; set; }
}
public class AmenityList
{
    public int Id { get; set; }
    public string Name { get; set; }
    public Item Status { get; set; }
}


public class AmenityFilter
{
    public string Name { get; set; }
    public int StatusId { get; set; }
}
