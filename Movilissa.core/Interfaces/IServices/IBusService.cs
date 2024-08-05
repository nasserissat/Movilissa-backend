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
    Task<IEnumerable<BusList>> FilterBus(BusFilter filter);


    Task<int> CreateAmenity(AmenityData data);
    Task<IEnumerable<AmenityList>> FilterAmenities(AmenityFilter filter);
    
    Task<IEnumerable<BusTypeList>> GetAllBusTypes();
    Task<int> CreateBusType(BusTypeData busTypeDTO);
    Task<int> UpdateBusType(int id, BusTypeData busTypeData);
    Task<int> DeleteBusType(int id);
    Task<IEnumerable<BusTypeList>> FilterBusTypes(BusTypeFilter filter);

    
    

}