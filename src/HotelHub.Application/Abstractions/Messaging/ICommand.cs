using HotelHub.Domain.Abstractions;
using MediatR;

namespace HotelHub.Application.Abstractions.Messaging;


public interface IBaseCommand
{
    
}

public interface ICommand : IRequest<Result>, IBaseCommand
{
    
}

public interface ICommand <TResponse> : IRequest<Result<TResponse>>, IBaseCommand
{
    
}