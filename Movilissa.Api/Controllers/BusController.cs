using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Movilissa_api.Enums;
using Movilissa_api.Models;
using Movilissa.core.DTOs.Bus.AmenityDTOs;
using Movilissa.core.Interfaces.IServices;

[ApiController]
[Route("api/bus")]
public class BusController : ControllerBase
{
    private readonly IGenericService<Amenity> _busAmenityService;

    public BusController(IGenericService<Amenity> busAmenityService)
    {
        _busAmenityService = busAmenityService;
    }

    // GET: api/bus/amenities
    [HttpGet("amenities")]
    public async Task<ActionResult<IEnumerable<Amenity>>> GetAllAmenities()
    {
        var amenities = await _busAmenityService.GetAll();
        return Ok(amenities);
    }

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
    public async Task<ActionResult<Amenity>> CreateAmenity([FromBody] AmenityData amenityData)
    {
        var newAmenity = new Amenity
        {
            Name = amenityData.Name,
            Status = (int)GenericStatus.Activo
        };
        await _busAmenityService.Add(newAmenity);
        return CreatedAtAction(nameof(GetAmenityById), new { id = newAmenity.Id }, newAmenity);
    }

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
        if (amenityData.Status.HasValue)
        {
            existingAmenity.Status = amenityData.Status.Value;
        }
        await _busAmenityService.Update(existingAmenity);
        
        return NoContent();
    }

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
}