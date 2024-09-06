﻿using buildingblock.Abstractions;
using buildingblock.Cache;
using MediatR;
using Microsoft.Extensions.Logging;

namespace buildingblock.Behaviours;

public class QueryCachingBehaviour<TRequest, TResponse>: IPipelineBehavior<TRequest, TResponse>
    where TRequest : ICachedQuery
    where TResponse : ResponseWrapper
{
    private readonly ICacheService _cacheService;
    private readonly ILogger<QueryCachingBehaviour<TRequest, TResponse>> _logger;

    public QueryCachingBehaviour(ICacheService cacheService, ILogger<QueryCachingBehaviour<TRequest, TResponse>> logger)
    {
        _cacheService = cacheService;
        _logger = logger;
    }

    public async  Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        TResponse? cachedResult = await _cacheService.GetAsync<TResponse>(request.CacheKey, cancellationToken);
        string name = typeof(TRequest).Name;

        if (cachedResult is not null)
        {
            _logger.LogInformation("Cache hit for {Query}", name);
            return cachedResult;
        }
        _logger.LogInformation("cache miss for {Query}", name);
        var result = await next();
        if (result.IsSuccessful)
        {
            await _cacheService.SetAsync(request.CacheKey, result, request.Expiration, cancellationToken);
        }

        return result;
    }
}