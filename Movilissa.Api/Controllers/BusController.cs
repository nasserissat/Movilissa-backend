using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Movilissa_api.Enums;
using Movilissa_api.Models;
using Movilissa.core.DTOs.Bus;
using Movilissa.core.DTOs.Bus.AmenityDTOs;
using Movilissa.core.DTOs.Shared;
using Movilissa.core.Interfaces.IServices;

[ApiController]
[Route("api/bus")]
public class BusController : ControllerBase
{
    private readonly IGenericService<Amenity> _busAmenityService;
    private readonly IGenericService<BusType> _busTypeService;
    private readonly IBusService _busService;
    
    public BusController(IGenericService<Amenity> busAmenityService, IBusService busService, IGenericService<BusType> busTypeService)
    {
        _busAmenityService = busAmenityService;
        _busService = busService;
        _busTypeService = busTypeService;
    }
    
    // CRUD Operations for Bus
    
    [AllowAnonymous]
    [HttpGet("")]
    public async Task<ActionResult<IEnumerable<BusList>>> GetAllBuses()
    {
        var buses = await _busService.GetAllBuses();
        return Ok(buses);
    }

    [AllowAnonymous]
    [HttpGet("{id}")]
    public async Task<ActionResult<BusDetailed>> GetBusById(int id)
    {
        var bus = await _busService.GetBusDetailed(id);
        if (bus == null)
        {
            return NotFound($"Bus with ID {id} not found.");
        }
        return Ok(bus);
    }

    [AllowAnonymous]
    [HttpPost("")]
    public async Task<IActionResult> CreateBus([FromBody] BusData busData)
    {
        var newBusId = await _busService.CreateBus(busData);
        return CreatedAtAction(nameof(GetBusById), new { id = newBusId }, busData);
    }

    [AllowAnonymous]
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateBus(int id, [FromBody] BusData busData)
    {
        var existingBus = await _busService.GetBusDetailed(id);
        if (existingBus == null)
        {
            return NotFound($"Bus with ID {id} not found.");
        }

        await _busService.UpdateBus(id, busData);
        return NoContent();
    }

    [AllowAnonymous]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteBus(int id)
    {
        var bus = await _busService.GetBusDetailed(id);
        if (bus == null)
        {
            return NotFound($"Bus with ID {id} not found.");
        }

        await _busService.DeleteBus(id);
        return NoContent();
    }

    [AllowAnonymous]
    [HttpGet("filter")]
    public async Task<ActionResult<IEnumerable<BusList>>> FilterBuses([FromQuery] BusFilter filter)
    {
        var buses = await _busService.FilterBus(filter);
        return Ok(buses);
    }
    
    // CRUD Operations for BusType

    [AllowAnonymous]
    [HttpGet("bus-types")]
    public async Task<ActionResult<IEnumerable<BusTypeList>>> GetAllBusTypes()
    {
        var busTypes = await _busService.GetAllBusTypes();
        return Ok(busTypes);
    }

    [AllowAnonymous]
    [HttpGet("bus-types/{id}")]
    public async Task<ActionResult<BusType>> GetBusTypeById(int id)
    {
        var busType = await _busTypeService.GetById(id);
        if (busType == null)
        {
            return NotFound($"BusType with ID {id} not found.");
        }
        return Ok(busType);
    }

    [AllowAnonymous]
    [HttpPost("bus-types")]
    public async Task<IActionResult> CreateBusType([FromBody] BusTypeData busTypeData)
    {
        var newBusTypeId = await _busService.CreateBusType(busTypeData);
        return CreatedAtAction(nameof(GetBusTypeById), new { id = newBusTypeId }, busTypeData);
    }

    [AllowAnonymous]
    [HttpPut("bus-types/{id}")]
    public async Task<IActionResult> UpdateBusType(int id, [FromBody] BusTypeData busTypeData)
    {
        var existingBusType = await _busTypeService.GetById(id);
        if (existingBusType == null)
        {
            return NotFound($"BusType with ID {id} not found.");
        }
        await _busService.UpdateBusType(id, busTypeData);
        return NoContent();
    }

    [AllowAnonymous]
    [HttpDelete("bus-types/{id}")]
    public async Task<IActionResult> DeleteBusType(int id)
    {
        var existingBusType = await _busTypeService.GetById(id);
        if (existingBusType == null)
        {
            return NotFound($"BusType with ID {id} not found.");
        }

        await _busTypeService.Delete(existingBusType);
        return NoContent();
    }
    [AllowAnonymous]
    [HttpGet("filtered-bus-types")]
    public async Task<ActionResult<IEnumerable<BusList>>> FilterBusTypes([FromQuery] BusTypeFilter filter)
    {
        var buses = await _busService.FilterBusTypes(filter);
        return Ok(buses);
    }

    
    
    [AllowAnonymous]

    // GET: api/bus/amenities
    [HttpGet("amenities")]
    public async Task<ActionResult<IEnumerable<AmenityList>>> GetAllAmenities()
    {
        var amenities = await _busAmenityService.GetAll();
        var amenityList = amenities.Select(a => new AmenityList
        {
            Id = a.Id,
            Name = a.Name,
            Status = new Item { Id = a.Status, Description = ((GenericStatus)a.Status).ToString() }
        }).ToList();

        return Ok(amenityList);
    }
    [AllowAnonymous]

    // GET: api/bus/amenities/{id}
    [HttpGet("amenities/{id}")]
    public async Task<ActionResult<Amenity>> GetAmenityById(int id)
    {
        var amenity = await _busAmenityService.GetById(id);
        if (amenity == null)
        {
            return NotFound("Amenity not found.");
        }
        return Ok(amenity);
    }
    [AllowAnonymous]

    // POST: api/bus/amenities
    [HttpPost("amenities")]
    public async  Task<IActionResult>  CreateAmenity([FromBody] AmenityData amenityData)
    {
        var newAmenity = await _busService.CreateAmenity(amenityData);
        return CreatedAtAction(nameof(GetAmenityById), new { id = newAmenity }, newAmenity);
    }
    [AllowAnonymous]

    // PUT: api/bus/amenities/{id}
    [HttpPut("amenities/{id}")]
    public async Task<IActionResult> UpdateAmenity(int id, [FromBody] AmenityData amenityData)
    {
        var existingAmenity = await _busAmenityService.GetById(id);
        if (existingAmenity == null)
        {
            return NotFound("Amenity not found.");
        }
        
        existingAmenity.Name = amenityData.Name;
        if (amenityData.StatusId.HasValue)
        {
            existingAmenity.Status = amenityData.StatusId.Value;
        }
        await _busAmenityService.Update(existingAmenity);
        
        return NoContent();
    }
    [AllowAnonymous]

    // DELETE: api/bus/amenities/{id}
    [HttpDelete("amenities/{id}")]
    public async Task<IActionResult> DeleteAmenity(int id)
    {
        var amenity = await _busAmenityService.GetById(id);
        if (amenity == null)
        {
            return NotFound("Amenity not found.");
        }
        
        await _busAmenityService.Delete(amenity);
        return NoContent();
    }
    [AllowAnonymous]

    [HttpGet("filtered-amenities")]
    public async Task<IActionResult> GetFilteredAmenities([FromQuery] AmenityFilter filter)
    {
        var amenities = await _busService.FilterAmenities(filter);
        return Ok(amenities);
    }
}