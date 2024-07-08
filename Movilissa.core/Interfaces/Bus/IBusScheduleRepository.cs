using Movilissa_api.Models;
using Movilissa.core.Interfaces;

namespace Movilissa_api.Data.IRepositories;

public interface IBusScheduleRepository : IGenericRepository<BusSchedule>
{
    Task<IEnumerable<BusSchedule>> GetAllBusSchedulesWithDetails();

}