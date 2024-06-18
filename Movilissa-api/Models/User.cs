namespace Movilissa_api.Models;

public class User
{
    public int UserId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }
    public int ProvinceId { get; set; }
    public int CountryId { get; set; }
    
    public virtual Province Province { get; set; }
    public virtual Country Country { get; set; }
    public virtual ICollection<Ticket> Tickets { get; set; }
    
}