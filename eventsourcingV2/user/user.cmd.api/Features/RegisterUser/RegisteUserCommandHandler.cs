using buildingblock.Abstractions;
using buildingblock.CQRS;

namespace user.cmd.api.Features.RegisterUser;

public class RegisteUserCommandHandler:ICommandHandler<RegisterUserCommand,Guid>
{
    public Task<ResponseWrapper<Guid>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}