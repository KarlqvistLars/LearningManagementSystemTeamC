namespace LearningManagementSystemTeamC.Application.Modules.Queries.GetModule;

public record GetModuleQuery(Guid CourseId, Guid UserId, string RoleCode);