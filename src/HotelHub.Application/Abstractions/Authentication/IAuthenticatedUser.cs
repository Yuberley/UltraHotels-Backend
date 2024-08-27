namespace HotelHub.Application.Abstractions.Authentication;

public interface IAuthenticatedUser
{
    Guid GetUserId();
}