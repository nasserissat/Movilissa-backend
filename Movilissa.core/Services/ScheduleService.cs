using Movilissa_api.Models;
using Movilissa.core.DTOs.Shared;
using Movilissa.core.DTOs.Ticket;
using Movilissa.core.Interfaces;
using Movilissa.core.Interfaces.IServices;
using Movilissa.core.Responses;

namespace Movilissa_api.Logic;

public class ScheduleService : IScheduleService
{
    private readonly IScheduleRepository _scheduleRepository;
    private readonly IGenericRepository<BusSchedule> _busScheduleRepository;

    public ScheduleService(IScheduleRepository scheduleRepository, IGenericRepository<BusSchedule> busScheduleRepository)
    {
        _scheduleRepository = scheduleRepository;
        _busScheduleRepository = busScheduleRepository;
    }

    public async Task<ServiceResponse<IEnumerable<TicketAvailableList>>> CheckTicketAvailability(TicketAvailabilityData data)
    {
        var response = new ServiceResponse<IEnumerable<TicketAvailableList>>();

        try
        {
            var schedules = await _scheduleRepository.GetAvailableSchedules(data);
            var busSchedules = await _busScheduleRepository.GetAll();

            var availableTickets = schedules.Select(s => new TicketAvailableList
            {
                Id = s.Id,
                Company = new Item { Id = s.CompanyId, Description = s.Company.Name },
                Origin = new Item { Id = s.Route.OriginId, Description = s.Route.Origin.Name },
                Destiny = new Item { Id = data.DestinyId, Description = s.Route.Destinations.FirstOrDefault(d => d.DestinationId == data.DestinyId)?.Destination.Name },
                Date = s.DepartureTime,
                DepartureTime = s.DepartureTime.ToString("hh:mm tt"),
                ArrivalTime = s.ArrivalTime?.ToString("hh:mm tt"),
                EstimatedDuration = s.EstimatedDuration,
                Price = s.Route.Destinations.FirstOrDefault(d => d.DestinationId == data.DestinyId)?.Price ?? 0,
                SeatsAvailable = busSchedules.FirstOrDefault(bs => bs.ScheduleId == s.Id)?.AvailableSeats ?? 0
            }).ToList();

            response.Data = availableTickets;
            response.IsSuccess = true;
            response.Message = "Boletos disponibles recuperados con éxito.";
        }
        catch (Exception ex)
        {
            response.IsSuccess = false;
            response.Message = $"Ocurrió un error: {ex.Message}";
            // Log the exception if necessary
        }

        return response;
    }
}