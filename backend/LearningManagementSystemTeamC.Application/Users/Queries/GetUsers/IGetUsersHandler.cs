using LearningManagementSystemTeamC.Application.Common.DTOs;

namespace LearningManagementSystemTeamC.Application.Users.Queries.GetUsers;

public interface IGetUsersHandler
{
    Task<IReadOnlyList<UserDto>> HandleAsync(GetUsersQuery query, CancellationToken cancellationToken);
}
