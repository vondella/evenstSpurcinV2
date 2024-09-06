using buildingblock.Abstractions;
using MediatR;

namespace buildingblock.CQRS;
public interface ICommandHandler<TCommand> : IRequestHandler<TCommand, ResponseWrapper>
    where TCommand : ICommand
{

}

public interface ICommandHandler<TCommand, TResponse> : IRequestHandler<TCommand, ResponseWrapper<TResponse>>
    where TCommand : ICommand<TResponse>
{

}