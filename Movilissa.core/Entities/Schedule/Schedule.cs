using Movilissa_api.Enums;

namespace Movilissa_api.Models;

public class Schedule
{
    public int Id { get; set; } 
    public int RouteId { get; set; }
    public int CompanyId { get; set; }

    public DateTime DepartureTime { get; set; }
    public DateTime? ArrivalTime { get; set; }
    public string? EstimatedDuration { get; set; }
    public List<DayOfWeek> DaysOfWeek { get; set; } // Día de la semana para horarios recurrentes
    public GenericStatus Status { get; set; } // Estado del viaje

    public Route Route { get; set; }
    public ICollection<BusSchedule> BusSchedules { get; set; }
    public virtual Company Company { get; set; }
}