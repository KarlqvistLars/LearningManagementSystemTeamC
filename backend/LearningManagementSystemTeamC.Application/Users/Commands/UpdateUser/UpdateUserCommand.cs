namespace LearningManagementSystemTeamC.Application.Users.Commands.UpdateUser;

public record UpdateUserCommand(
    Guid UserId,
    string Email,
    string FirstName,
    string LastName,
    DateTime? DateOfBirth,
    string? PhoneNumber,
    string? Address,
    string? PostalCode,
    string? City);