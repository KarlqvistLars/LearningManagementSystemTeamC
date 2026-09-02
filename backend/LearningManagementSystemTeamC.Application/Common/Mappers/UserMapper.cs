using LearningManagementSystemTeamC.Application.Common.DTOs;
using LearningManagementSystemTeamC.Domain.Users;
using System.Linq.Expressions;

namespace LearningManagementSystemTeamC.Application.Common.Mappers;

public class UserMapper
{
    public static UserDto ToDto(User user)
    {
        return new UserDto(user.Id, user.Email);
    }

    public static readonly Expression<Func<User, UserDto>> Projection = user => new UserDto(user.Id, user.Email);
}
