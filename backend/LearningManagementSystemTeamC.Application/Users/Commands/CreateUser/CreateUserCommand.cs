namespace LearningManagementSystemTeamC.Application.Users.Commands.CreateUser;

public record CreateUserCommand(string Email, Guid RoleId);