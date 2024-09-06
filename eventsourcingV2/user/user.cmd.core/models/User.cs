namespace user.cmd.core.models;

public sealed class User
{
    private readonly List<Role> roles = new List<Role>();
    public Guid Id { get; set; }
    public string firstname { get; set; }
    public string lastname { get; set; }
    public string email { get; set; }
    public string identityId { get; set; }
    public IReadOnlyCollection<Role> Roles => roles.ToList();
    private User()
    {

    }
    //public static User CreateUser(FirstName firtsname, LastName lastname, Email email)
    //{
    //    var user = new User(Guid.NewGuid(), firtsname, lastname, email);
    //    user.RaiseDomainEvent(new UserCreatedDomainEvent(user.Id));
    //    user.roles.Add(Role.Registered);
    //    return user;
    //}

    public void SetIdentityId(string identityId)
    {
        identityId = identityId;
    }
}