namespace Movilissa_api.Models;

public class InvoiceDetail
{
    public int Id { get; set; }
    public int InvoiceId { get; set; }
    public int TicketId { get; set; }
    public decimal Quantity { get; set; }
    public decimal SubtotalAmount { get; set; }
    public decimal TaxAmount { get; set; }
    public decimal TotalAmount  { get; set; }
    public Ticket Ticket { get; set; }
    public Invoice Invoice { get; set; }
    public int CompanyId { get; set; }
    public virtual Company Company { get; set; }
}