using buildingblock.Abstractions;
using MediatR;

namespace buildingblock.CQRS;

public interface IQueryHandler<TQuery, TResponse> : IRequestHandler<TQuery, ResponseWrapper<TResponse>>
    where TQuery : IQuery<TResponse>
{

}