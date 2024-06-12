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
}