namespace HotelHub.Application.Abstractions.Email;

public interface IEmailService
{
    Task SendAsync(Domain.SharedValueObjects.Email recipient, string subject, string body);
}