namespace LearningManagementSystemTeamC.Application.Common.DTOs;

public record LoginResultDto
(
    string AccessToken,
    int ExpiresInMinutes,
    UserDto User
);

