using Microsoft.AspNetCore.Identity;
using Movilissa_api.Enums;

namespace Movilissa_api.Models;

public class User : IdentityUser
{
    public int? CompanyId { get; set; } 
    public virtual Company Company { get; set; }
    public UserTypeEnum UserType { get; set; }
    
    public virtual ICollection<Ticket> Tickets { get; set; }
    
}