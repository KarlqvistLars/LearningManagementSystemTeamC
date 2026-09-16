namespace LearningManagementSystemTeamC.Application.Common.DTOs;

public record UserSimplifiedDto(
    Guid Id,
    string FirstName,
    string LastName
);