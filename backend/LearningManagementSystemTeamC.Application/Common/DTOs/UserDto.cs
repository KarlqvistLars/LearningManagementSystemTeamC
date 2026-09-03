namespace LearningManagementSystemTeamC.Application.Common.DTOs;

public record UserDto(Guid Id, string Email, Guid RoleId, string RoleName);