using Asp.Versioning;
using HotelHub.Application.Bookings.GetAllBookings;
using HotelHub.Application.Bookings.GetBooking;
using HotelHub.Application.Bookings.ReserveRoom;
using HotelHub.Domain.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HotelHub.WebAPI.Controllers.Booking;

[Authorize]
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
    
    
    [HttpGet, Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetBookings(CancellationToken cancellationToken)
    {
        var query = new GetAllBookingQuery();

        Result<IEnumerable<BookingsResponse>> result = await _sender.Send(query, cancellationToken);

        return result.IsSuccess ? Ok(result.Value) : NotFound();
    }
    
    [HttpGet("{id}"), Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetBooking(Guid id, CancellationToken cancellationToken)
    {
        var query = new GetBookingQuery(id);

        Result<BookingResponse> result = await _sender.Send(query, cancellationToken);

        return result.IsSuccess ? Ok(result.Value) : NotFound();
    }
    

    [HttpPost, Authorize(Roles = "User")]
    public async Task<IActionResult> ReserveBooking(
        ReserveBookingRequest request,
        CancellationToken cancellationToken)
    {
        var command = new ReserveBookingCommand(
            request.RoomId,
            request.UserId,
            request.StartDate,
            request.EndDate,
            request.EmergencyContactFullName,
            request.EmergencyContactPhoneNumber,
            request.Guests.Select(g => new GuestCommand(
                g.FirstName,
                g.LastName,
                g.Email,
                g.TypeDocument,
                g.Document,
                g.Phone,
                g.Gender,
                g.BirthDate
            )).ToList()
            );

        Result<Guid> result = await _sender.Send(command, cancellationToken);

        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }

        return CreatedAtAction(nameof(GetBooking), new { id = result.Value }, result.Value);
    }
}