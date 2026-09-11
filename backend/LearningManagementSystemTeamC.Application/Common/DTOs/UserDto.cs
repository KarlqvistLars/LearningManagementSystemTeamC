namespace LearningManagementSystemTeamC.Application.Common.DTOs;

public record UserDto(Guid Id, string Email, string FirstName, string LastName, Guid RoleId, string RoleName, bool IsActive);