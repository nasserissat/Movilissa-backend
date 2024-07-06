namespace Movilissa_api.Models;

public class Branch
{
    public int Id { get; set; }
    public string Name { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public int CompanyId { get; set; }
    public int ProvinceId { get; set; }
    
    public virtual Province Province { get; set; }
    public virtual Company Company { get; set; }
}