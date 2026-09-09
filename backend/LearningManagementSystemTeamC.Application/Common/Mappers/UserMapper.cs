using LearningManagementSystemTeamC.Application.Common.DTOs;
using LearningManagementSystemTeamC.Domain.Roles;
using LearningManagementSystemTeamC.Domain.Users;

namespace LearningManagementSystemTeamC.Application.Common.Mappers;

public class UserMapper
{
    public static UserDto ToDto(User user, Role role, string firstName, string lastName)
    {
        return new UserDto(user.Id, user.Email, firstName, lastName, role.Id, role.Name);
    }
}
