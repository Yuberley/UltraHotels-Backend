using Asp.Versioning;
using HotelHub.Application.Bookings.GetAllBookings;
using HotelHub.Application.Bookings.GetBooking;
using HotelHub.Application.Bookings.ReserveRoom;
using HotelHub.Domain.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HotelHub.WebAPI.Controllers.Booking;

[ApiController]
[ApiVersion(ApiVersions.V1)]
[Route("api/v{version:apiVersion}/bookings")]
public class BookingsController : ControllerBase
{
    private readonly ISender _sender;

    public BookingsController(ISender sender)
    {
        _sender = sender;
    }
    
    
    [HttpGet]
    public async Task<IActionResult> GetBookings(CancellationToken cancellationToken)
    {
        var query = new GetAllBookingQuery();

        Result<IEnumerable<BookingsResponse>> result = await _sender.Send(query, cancellationToken);

        return result.IsSuccess ? Ok(result.Value) : NotFound();
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetBooking(Guid id, CancellationToken cancellationToken)
    {
        var query = new GetBookingQuery(id);

        Result<BookingResponse> result = await _sender.Send(query, cancellationToken);

        return result.IsSuccess ? Ok(result.Value) : NotFound();
    }
    

    [HttpPost]
    public async Task<IActionResult> ReserveBooking(
        ReserveBookingRequest request,
        CancellationToken cancellationToken)
    {
        var command = new ReserveBookingCommand(
            request.RoomId,
            request.UserId,
            request.StartDate,
            request.EndDate);

        Result<Guid> result = await _sender.Send(command, cancellationToken);

        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }

        return CreatedAtAction(nameof(GetBooking), new { id = result.Value }, result.Value);
    }
}