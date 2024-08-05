using Movilissa_api.Models;
using Movilissa.core.DTOs.Bus;
using Movilissa.core.DTOs.Bus.AmenityDTOs;
using Movilissa.core.Interfaces;

namespace Movilissa_api.Data.IRepositories;

public interface IBusRepository : IGenericRepository<Bus>
{ 
    Task<Bus> GetBusWithDetails(int busId);
    Task<List<BusList>> GetBusList();

    Task<IEnumerable<BusList>> FilterBus(BusFilter filter);

    Task<IEnumerable<Amenity>> FilterAmenities(AmenityFilter filter);
    Task<IEnumerable<BusTypeList>> FilterBusTypes(BusTypeFilter filter);


    


}