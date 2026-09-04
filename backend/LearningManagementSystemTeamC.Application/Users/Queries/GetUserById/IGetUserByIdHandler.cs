using LearningManagementSystemTeamC.Application.Common.DTOs;

namespace LearningManagementSystemTeamC.Application.Users.Queries.GetUserById;

public interface IGetUserByIdHandler
{
    Task<UserDto> Handle(GetUserByIdQuery query, CancellationToken cancellationToken);
}
