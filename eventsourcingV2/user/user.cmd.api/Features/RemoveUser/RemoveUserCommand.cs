using buildingblock.CQRS;
namespace user.cmd.api.Features.RemoveUser;

public record RemoveUserCommand(Guid Id):ICommand<Guid>;