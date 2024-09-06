using user.cmd.core.events;

namespace user.cmd.api.Shared.Domains.Aggregates;

public sealed class UserAggregates:AggregateRoot
{
    private bool _active;
    
    public bool Active
    {
        get => _active; set => _active = value;
    }
    public UserAggregates()
    {
        
    }

    public UserAggregates(Guid Id, string Firstname, string Lastname, string Email, string Password)
    {
        RaiseEvent(new UserRegisteredEvent 
        { 
              Id = Id,
              Firstname = Firstname,
              Lastname=Lastname,
              Email=Email,
              Password=Password
        });
    }
    public void Apply(UserRegisteredEvent @event)
    {
        id = @event.Id;
        _active = true;
    }

    public void UpdateUser(Guid Id, string Firstname, string Lastname)
    {
        if (!_active)
        {
            throw new InvalidOperationException("you can not edit a comment of inactive post");
        }
        RaiseEvent(new UserUpdatedEvent
        {
             Id=Id,
             Firstname=Firstname,
             Lastname=Lastname
        });
    }

    public void Apply(UserUpdatedEvent @event)
    {
        id = @event.Id;
    }

    public void RemoveUser(Guid Id)
    {
        if (!_active)
        {
            throw new InvalidOperationException("you can not edit a comment of inactive post");
        }
        RaiseEvent(new UserRemovedEvent
        {
            Id = Id,
          
        });
    }

    public void Apply(UserRemovedEvent @event)
    {
        id = @event.Id;
    }

}