namespace LearningManagementSystemTeamC.Application.Users.Commands.CreateUser;

public record CreateUserCommand(string Email, string Password, Guid RoleId);