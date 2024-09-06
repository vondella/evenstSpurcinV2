﻿using buildingblock.Config;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using user.cmd.api.Shared.Configuration;
using user.cmd.api.Shared.Domains;

namespace user.cmd.api.Shared.Repository;

public class EventStoreRepository:IEventStoreRepository
{
    private readonly IMongoCollection<EventModel> _eventStoreCollection;

    public EventStoreRepository(IOptions<MongoDbConfig> config)
    {
        var mongoClient = new MongoClient(config.Value.ConnectionString);
        var mongoDatabase = mongoClient.GetDatabase(config.Value.Database);
        _eventStoreCollection = mongoDatabase.GetCollection<EventModel>(config.Value.Collection);

    }
    public async  Task SaveAsync(EventModel @event)
    {
        await _eventStoreCollection.InsertOneAsync(@event).ConfigureAwait(false);
    }

    public async  Task<List<EventModel>> FindByAggregate(Guid aggregateId)
    {
        return await _eventStoreCollection.Find(x => x.AggregateIdentifier == aggregateId).ToListAsync().ConfigureAwait(false);

    }

    public async  Task<List<EventModel>> FindAllAsync()
    {
        return await _eventStoreCollection.Find(_ => true).ToListAsync().ConfigureAwait(false);

    }
}