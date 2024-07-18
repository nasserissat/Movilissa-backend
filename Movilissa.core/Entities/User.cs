using Microsoft.AspNetCore.Identity;

namespace Movilissa_api.Models;

public class User: IdentityUser
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public int? CompanyId { get; set; }
    public virtual Company Company { get; set; }
    
    public virtual ICollection<Ticket> Tickets { get; set; }
    
}