using Asp.Versioning;
using HotelHub.Application.Rooms;
using HotelHub.Application.Rooms.CreateRoom;
using HotelHub.Application.Rooms.GetRooms;
using HotelHub.Application.Rooms.SearchRooms;
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
    
    [HttpPost]
    public async Task<IActionResult> AddRoom(
        AddRoomRequest request,
        CancellationToken cancellationToken)
    {
        var command = new AddRoomCommand(
            request.HotelId,
            request.RoomNumber,
            request.Type,
            request.NumberGuestsAdults,
            request.NumberGuestsChildren,
            request.BaseCost,
            request.Currency,
            request.Taxes,
            request.IsActive);
        
        Result<Guid> result = await _sender.Send(command, cancellationToken);
        
        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }
        
        return Ok(result.Value);    
    }
    
    
    [HttpGet("search")]
    public async Task<IActionResult> SearchRooms(
        SearchRoomsRequest request,
        CancellationToken cancellationToken)
    {
        var query = new SearchRoomsQuery(
            request.StartDate,
            request.EndDate,
            request.NumberAdults,
            request.NumberChildren,
            request.Country);
        
        Result<IReadOnlyList<RoomSearchResponse>> result = await _sender.Send(query, cancellationToken);
        
        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }
        
        return Ok(result.Value);
    }
}