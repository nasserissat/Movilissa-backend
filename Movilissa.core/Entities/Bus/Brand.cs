namespace Movilissa_api.Models;

public class Brand
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int CompanyId { get; set; }
    
    public virtual ICollection<BusType> BusTypes {get; set; }
    public virtual Company Company { get; set; }
}