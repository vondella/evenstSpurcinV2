namespace user.cmd.core.models;

public class Role
{
    public static readonly Role Registered = new(Guid.NewGuid(), "Registered");

    public Role(Guid id, string name)
    {
        Id = id;
        Name = name;
    }
    public Guid Id { get; init; }

    public string Name { get; init; }

    public ICollection<User> Users { get; init; } = new List<User>();
}