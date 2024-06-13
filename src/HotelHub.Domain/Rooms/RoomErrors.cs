using HotelHub.Domain.Abstractions;

namespace HotelHub.Domain.Rooms;

public class RoomErrors
{
    public static Error NotFound => new(
        "Rooms.NotFound", 
        $"The room was not found");
    
    public static Error RoomIsDisabled => new(
        "Rooms.RoomIsDisabled", 
        $"The room is disabled");
    
    public static Error ExcessiveGuests => new(
        "Rooms.ExcessiveGuests", 
        $"The room does not support the number of guests");
    
    public static Error WrongDates => new(
        "Rooms.WrongDates", 
        $"The dates are invalid");
    
    public static readonly Error InvalidStartDate = new(
        "Booking.InvalidStartDate",
        "The start date must be greater than the current date");
    
    public static readonly Error InvalidEndDate = new(
        "Booking.InvalidEndDate",
        "The end date must be greater than the current date");
    
    public static readonly Error NumberOfGuestsMustBeGreaterThanZero = new(
        "Booking.NumberOfGuestsMustBeGreaterThanZero",
        "The number of guests must be greater than zero");
}