namespace Movilissa_api.Models;

public class BusSchedule
{
    public int Id { get; set; }
    public int BusId { get; set; }
    public int ScheduleId { get; set; }
    public int AvailableSeats { get; set; } // Asientos disponibles para este horario específico.
    public BusScheduleStatus Status { get; set; }
    
    public Bus Bus { get; set; }
    public Schedule Schedule { get; set; }
    
}
public enum BusScheduleStatus
{
    Ready = 1,
    InTransit,
    Completed,
    Delayed,
    Cancelled
}