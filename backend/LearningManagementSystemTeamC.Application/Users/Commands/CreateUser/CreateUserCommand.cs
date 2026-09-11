namespace LearningManagementSystemTeamC.Application.Users.Commands.CreateUser;

public record CreateUserCommand(
    string Email,
    string Password,
    Guid RoleId,
    string FirstName,
    string LastName,
    DateTime? DateOfBirth,
    string? PhoneNumber,
    string? Address,
    string? PostalCode,
    string? City);