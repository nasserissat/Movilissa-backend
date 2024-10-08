namespace Movilissa_api.Models;

public class Payment
{
    public int PaymentId { get; set; }
    public DateTime PaymentDate { get; set; }
    public decimal Amount { get; set; }
    public string PaymentMethod { get; set; }
    public string UserId { get; set; }
    public int TicketId { get; set; }
    public int CompanyId { get; set; }
    public User User { get; set; }
    public Ticket Ticket { get; set; } 
    public virtual Company Company { get; set; }

}
