using HotelHub.Application.Abstractions.Authentication;
using HotelHub.Application.Abstractions.Messaging;
using HotelHub.Domain.Abstractions;

namespace HotelHub.Application.Users.LoginUser;

internal sealed class LogInUserCommandHandler : ICommandHandler<LogInUserCommand, AccessTokenResponse>
{
    private readonly IJwtService _jwtService;
    
    public LogInUserCommandHandler(IJwtService jwtService)
    {
        _jwtService = jwtService;
    }
    
    public Task<Result<AccessTokenResponse>> Handle(LogInUserCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}