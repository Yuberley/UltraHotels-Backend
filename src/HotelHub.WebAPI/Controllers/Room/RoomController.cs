using Asp.Versioning;
using HotelHub.Application.Rooms.Queries;
using HotelHub.Application.Rooms.Queries.GetAll;
using HotelHub.Domain.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HotelHub.WebAPI.Controllers.Room;

[ApiController]
[ApiVersion(ApiVersions.V1)]
[Route("api/v{version:apiVersion}/rooms")]
public class RoomController : ControllerBase
{
    private readonly ISender _sender;
    
    public RoomController(ISender sender)
    {
        _sender = sender;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var query = new GetAllRoomsQuery();
        Result<IEnumerable<RoomResponse>> result = await _sender.Send(query, cancellationToken);
        
        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }
        
        return Ok(result.Value);
    }
}