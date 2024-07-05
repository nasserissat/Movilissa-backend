namespace Movilissa_api.Models;

public class Schedule
{
    public int Id { get; set; } 
    public DateTime DepartureDateTime { get; set; }
    public DateTime ArrivalDateTime { get; set; }
    public int RouteId { get; set; }
    public int CompanyId { get; set; }
    public int? BusId { get; set; }
    public Bus? Bus { get; set; } // Será requerido cuando me pueda integrar a las empresas de autobuses oficialmente
    public Route Route { get; set; }
    public decimal Fare { get; set; } // Base fare
    public virtual Company Company { get; set; }
}