using LearningManagementSystemTeamC.Application.Common.Interfaces;

namespace LearningManagementSystemTeamC.Application.Users.Commands.CreateUser;

public class CreateUserHandler
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateUserHandler(IUserRepository userRepository, IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
    }
    public async Task Handle(CreateUserCommand command, CancellationToken cancellationToken)
    {
        // check if role exists by id
        // if not, throw RoleNotFound

        // check if user exists by email
        // if yes, throw EmailRegistered

        // new user creation
        // userRepo add
        // unitOfWork save

        // return new UserDto
        //await _userRepository.AddAsync(user, cancellationToken);
    }
}
