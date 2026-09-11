namespace LearningManagementSystemTeamC.Application.Common.DTOs;

public record UserDto(
    Guid Id,
    string Email,
    string FirstName,
    string LastName,
    DateTime? DateOfBirth,
    string? PhoneNumber,
    string? Address,
    string? PostalCode,
    string? City,
    Guid RoleId,
    string RoleName,
    bool IsActive,
    DateTime CreatedAt
);