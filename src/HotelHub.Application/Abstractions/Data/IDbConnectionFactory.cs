using System.Data;

namespace HotelHub.Application.Abstractions.Data;

public interface IDbConnectionFactory
{
    IDbConnection CreateConnection();
}