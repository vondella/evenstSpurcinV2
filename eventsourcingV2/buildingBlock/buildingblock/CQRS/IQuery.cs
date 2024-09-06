using buildingblock.Abstractions;
using MediatR;

namespace buildingblock.CQRS;

public interface IQuery<TResponse> : IRequest<ResponseWrapper<TResponse>>
{

}