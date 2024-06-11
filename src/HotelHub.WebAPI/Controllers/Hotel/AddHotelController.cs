using Asp.Versioning;
using HotelHub.Application.Hotels.Create;
using HotelHub.Domain.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HotelHub.WebAPI.Controllers.Hotel;

[ApiController]
[ApiVersion(ApiVersions.V1)]
[Route("api/v{version:apiVersion}/hotels")]
public class AddHotelController : ControllerBase
{
    private readonly ISender _sender;
    
    public AddHotelController(ISender sender)
    {
        _sender = sender;
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
};