using buildingblock.CQRS;

namespace user.cmd.api.Features.RegisterUser;
public record RegisterUserCommand(string Firstname,string Lastname,string Email,string Password):ICommand<Guid>;