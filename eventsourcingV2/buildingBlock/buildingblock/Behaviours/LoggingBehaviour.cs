using buildingblock.Abstractions;
using MediatR;
using Microsoft.Extensions.Logging;

namespace buildingblock.Behaviours;

public sealed class LoggingBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
where TRequest : IBaseRequest
where TResponse : ResponseWrapper
{
    private readonly ILogger<LoggingBehaviour<TRequest, TResponse>> _logger;

    public async  Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var name = request.GetType().Name;
        try
        {
            _logger.LogInformation("Executing command {command}", name);
            var result = await next();
            _logger.LogInformation("Command {command} processed successfully", name);
            return result;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Command {command} processing failed", name);
            throw;
        }
    }
}