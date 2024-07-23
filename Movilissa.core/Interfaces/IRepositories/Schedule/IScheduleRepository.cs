using Movilissa_api.Models;
using Movilissa.core.DTOs.Ticket;

namespace Movilissa.core.Interfaces.IServices;

public interface IScheduleRepository
{
    Task<IEnumerable<Schedule>> GetAvailableSchedules(TicketAvailabilityData data);

}