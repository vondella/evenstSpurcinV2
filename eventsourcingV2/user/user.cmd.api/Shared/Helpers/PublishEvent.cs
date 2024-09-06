using Mapster;
using MassTransit;
using user.cmd.core.events;

namespace user.cmd.api.Shared.Helpers;

public static  class PublishEvent
{
    public static async Task PublishAsync(IPublishEndpoint publishEndpoint, BaseEvent baseEvent)
    {
        switch (baseEvent.GetType().Name)
        {
            case nameof(UserUpdatedEvent):
                await publishEndpoint.Publish(baseEvent.Adapt<UserUpdatedEvent>());
                break;
            case nameof(UserRegisteredEvent):
                await publishEndpoint.Publish(baseEvent.Adapt<UserRegisteredEvent>());
                break;
            default:
                await publishEndpoint.Publish(baseEvent.Adapt<UserRemovedEvent>());
                break;
        }
    }
}