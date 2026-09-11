using LearningManagementSystemTeamC.Application.Common.Interfaces;
using LearningManagementSystemTeamC.Domain.Common.Exceptions;
using LearningManagementSystemTeamC.Domain.Users;

namespace LearningManagementSystemTeamC.Application.Users.Commands.DeleteUser;

internal class DeleteUserHandler : IDeleteUserHandler
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteUserHandler(IUserRepository userRepository, IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task HandleAsync(
        DeleteUserCommand deleteUserCommand,
        CancellationToken cancellationToken)
    {
        var existingUser = await _userRepository.GetByIdAsync(deleteUserCommand.UserId, cancellationToken) ?? throw new NotFoundException(UserRules.UserNotFoundCode, UserRules.UserNotFoundMessage);

        existingUser.Disable();

        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}