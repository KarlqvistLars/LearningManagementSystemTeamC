using LearningManagementSystemTeamC.Application.Common.DTOs;

namespace LearningManagementSystemTeamC.Application.Users.Queries.GetUserById;

public interface IGetUserByIdHandler
{
    Task<UserDto> HandleAsync(GetUserByIdQuery query, CancellationToken cancellationToken);
}
