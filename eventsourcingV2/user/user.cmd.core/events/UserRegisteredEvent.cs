namespace user.cmd.core.events;

public sealed class UserRegisteredEvent:BaseEvent
{
    public UserRegisteredEvent() : base(nameof(UserRegisteredEvent))
    {
    }
    public string Firstname { get; set; }
    public string Lastname { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
}