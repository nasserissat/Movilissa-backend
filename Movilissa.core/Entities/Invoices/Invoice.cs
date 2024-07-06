namespace Movilissa_api.Models;

public class Invoice
{
    public int Id { get; set; }
    public string InvoiceNumber { get; set; }
    public DateTime InvoiceDate { get; set; }
    public decimal TotalAmount { get; set; }
    public string NCFNumber { get; set; }
    public string TaxRegistrationNumber { get; set; }
    public int StatusId { get; private set; }

    public int UserId { get; set; }
    public int PaymentId { get; set; }

    public User User { get; set; }
    public virtual Payment Payment { get; set; }
    public virtual InvoiceStatus Status { get; set; }
    public ICollection<InvoiceDetail> InvoiceDetails { get; set; }
}