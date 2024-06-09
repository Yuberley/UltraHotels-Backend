using HotelHub.Application.Abstractions.Messaging;
using HotelHub.Domain.Abstractions;

namespace HotelHub.Application.Users.RegisterUser;

internal sealed class RegisterUserCommandHandler : ICommandHandler<RegisterUserCommand, Guid>
{
    private readonly IUnitOfWork _unitOfWork;
    
    public RegisterUserCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    
    
    public Task<Result<Guid>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}