using Movilissa.core.DTOs.Bus;
using Movilissa.core.DTOs.Bus.AmenityDTOs;

namespace Movilissa.core.Interfaces.IServices;

public interface IBusService
{
    Task<IEnumerable<BusList>> GetAllBuses();
    Task<BusDetailed> GetBusDetailed(int id);
    Task<int> CreateBus(BusData busDTO);
    Task UpdateBus(int id, BusData busData);
    Task DeleteBus(int id);

    Task<int> CreateAmenity(AmenityData data);
    Task<IEnumerable<AmenityList>> GetAllAmenities();
    Task<int> UpdateAmenity(int id, AmenityData data);
    Task<int> DeleteAmenity(int id);
    Task<IEnumerable<AmenityList>> FilterAmenities(AmenityFilter filter);

}