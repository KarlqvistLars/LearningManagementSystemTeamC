using LearningManagementSystemTeamC.Domain.Users;

namespace LearningManagementSystemTeamC.Application.Users;

public interface IUserRepository
{
    Task AddAsync(User user, CancellationToken cancellationToken);
}