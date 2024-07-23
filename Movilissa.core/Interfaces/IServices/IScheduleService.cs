using Movilissa.core.DTOs.Ticket;
using Movilissa.core.Responses;

namespace Movilissa.core.Interfaces.IServices;

public interface IScheduleService
{
    Task<ServiceResponse<IEnumerable<TicketAvailableList>>> CheckTicketAvailability(TicketAvailabilityData data);
}