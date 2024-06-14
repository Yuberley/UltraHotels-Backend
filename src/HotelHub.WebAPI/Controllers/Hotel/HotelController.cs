using Asp.Versioning;
using HotelHub.Application.Hotels;
using HotelHub.Application.Hotels.CreateHotel;
using HotelHub.Application.Hotels.GetHotel;
using HotelHub.Application.Hotels.GetHotels;
using HotelHub.Application.Hotels.UpdateHotel;
using HotelHub.Domain.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HotelHub.WebAPI.Controllers.Hotel;

[Authorize(Roles = "Admin")]
[ApiController]
[ApiVersion(ApiVersions.V1)]
[Route("api/v{version:apiVersion}/hotels")]
public class HotelController : ControllerBase
{
    private readonly ISender _sender;
    
    public HotelController(ISender sender)
    {
        _sender = sender;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var query = new GetAllHotelsQuery();
        Result<IEnumerable<HotelResponse>> result = await _sender.Send(query, cancellationToken);
        
        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }
        
        return Ok(result.Value);
    }
    
    [HttpPost]
    public async Task<IActionResult> AddHotel(
        AddHotelRequest request,
        CancellationToken cancellationToken)
    {
        var command = new AddHotelCommand(
            request.Name,
            request.Description,
            request.Country,
            request.State,
            request.City,
            request.ZipCode,
            request.Street,
            request.IsActive);
        
        Result<Guid> result = await _sender.Send(command, cancellationToken);
        
        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }
        
        return Ok(result.Value);
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetHotel(Guid id, CancellationToken cancellationToken)
    {
        var query = new GetHotelByIdQuery(id);
        Result<HotelResponse> result = await _sender.Send(query, cancellationToken);
        
        if (result.IsFailure)
        {
            return NotFound();
        }
        
        return Ok(result.Value);
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateHotel(
        Guid id,
        AddHotelRequest request,
        CancellationToken cancellationToken)
    {
        var command = new UpdateHotelCommand(
            id,
            request.Name,
            request.Description,
            request.Country,
            request.State,
            request.City,
            request.ZipCode,
            request.Street,
            request.IsActive);
        
        Result result = await _sender.Send(command, cancellationToken);
        
        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }
        
        return Ok();
    }
}