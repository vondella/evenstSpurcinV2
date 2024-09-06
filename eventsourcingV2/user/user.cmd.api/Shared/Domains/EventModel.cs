﻿using MongoDB.Bson.Serialization.Attributes;
using user.cmd.core.events;

namespace user.cmd.api.Shared.Domains;

public class EventModel
{
    [BsonId]
    [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
    public string Id { get; set; }
    public DateTime Timestamp { get; set; }
    public Guid AggregateIdentifier { get; set; }
    public string AggregateType { get; set; }
    public int Version { get; set; }
    public string EventType { get; set; }
    public BaseEvent EvenData { get; set; }
}