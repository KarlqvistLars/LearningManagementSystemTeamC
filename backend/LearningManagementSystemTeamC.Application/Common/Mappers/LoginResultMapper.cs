using LearningManagementSystemTeamC.Application.Common.DTOs;

namespace LearningManagementSystemTeamC.Application.Common.Mappers;

public class LoginResultMapper
{
    public static LoginResultDto ToDto(string token, int expiresInMin, UserDto user)
    {
        return new LoginResultDto(token, expiresInMin, user);
    }
}
