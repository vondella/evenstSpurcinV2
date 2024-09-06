using buildingblock.Abstractions;
using MediatR;

namespace buildingblock.CQRS;

public interface ICommand : IRequest<ResponseWrapper>, IBaseCommand
{

}

public interface ICommand<TResponse> : IRequest<ResponseWrapper<TResponse>>, IBaseCommand
{

}

public interface IBaseCommand
{

}