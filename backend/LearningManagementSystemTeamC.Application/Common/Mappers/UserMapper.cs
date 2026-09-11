using LearningManagementSystemTeamC.Application.Common.DTOs;
using LearningManagementSystemTeamC.Domain.Roles;
using LearningManagementSystemTeamC.Domain.UserInfos;
using LearningManagementSystemTeamC.Domain.Users;

namespace LearningManagementSystemTeamC.Application.Common.Mappers;

public static class UserMapper
{
    public static UserDto ToDto(User user, Role role, UserInfo userInfo)
    {
        return new UserDto(
            user.Id,
            user.Email,
            userInfo.FirstName,
            userInfo.LastName,
            userInfo.DateOfBirth,
            userInfo.PhoneNumber,
            userInfo.Address,
            userInfo.PostalCode,
            userInfo.City,
            role.Id,
            role.Name,
            user.IsActive,
            user.CreatedAt
        );
    }
}