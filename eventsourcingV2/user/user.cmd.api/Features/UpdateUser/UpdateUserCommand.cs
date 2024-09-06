using buildingblock.CQRS;

namespace user.cmd.api.Features.UpdateUser;

public record UpdateUserCommand(Guid Id,string Firstname, string Lastname, string Email, string Password) :ICommand<Guid>;