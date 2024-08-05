using System.Security.Principal;

namespace Movilissa_api.Models;

public class BusType
{
    public int Id { get; set; }
    public int BrandId { get; set; }
    public string Model { get; set; }
    public int SeatingCapacity { get; set; }
    public int Status { get; set; }

    public int CompanyId { get; set; }
    public virtual Company Company { get; set; }
    public virtual Brand Brand { get; set; }

    
}