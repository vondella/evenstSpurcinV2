namespace user.cmd.core.events;

public sealed class UserUpdatedEvent:BaseEvent
{
    public UserUpdatedEvent() : base(nameof(UserUpdatedEvent))
    {
    }
    public Guid Id { get; set; }
    public string Firstname { get; set; }
    public string Lastname { get; set; }
}