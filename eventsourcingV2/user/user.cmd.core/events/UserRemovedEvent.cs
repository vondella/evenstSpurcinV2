namespace user.cmd.core.events;

public class UserRemovedEvent:BaseEvent
{
    public UserRemovedEvent() : base(nameof(UserRemovedEvent))
    {
    }
    public Guid Id { get; set; }
}