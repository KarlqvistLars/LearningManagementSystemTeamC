using LearningManagementSystemTeamC.Application.Common.Interfaces;
using LearningManagementSystemTeamC.Domain.Common.Exceptions;
using LearningManagementSystemTeamC.Domain.Users;

namespace LearningManagementSystemTeamC.Application.Users.Commands.ToggleUserStatus;

public class ToggleUserStatusHandler : IToggleUserStatusHandler
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;

    public ToggleUserStatusHandler(IUserRepository userRepository, IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task HandleAsync(
        ToggleUserStatusCommand command,
        CancellationToken cancellationToken)
    {
        var existingUser = await _userRepository.GetByIdAsync(
            command.UserId,
            cancellationToken) ?? throw new NotFoundException(UserRules.UserNotFoundCode, UserRules.UserNotFoundMessage);

        existingUser.ToggleStatus();

        await _unitOfWork.SaveChangesAsync();
    }
}