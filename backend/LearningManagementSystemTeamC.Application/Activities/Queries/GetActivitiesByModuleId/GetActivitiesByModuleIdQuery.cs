namespace LearningManagementSystemTeamC.Application.Activities.Queries.GetActivitiesByModuleId;

public record GetActivitiesByModuleIdQuery(Guid ModuleId, Guid UserId, string RoleCode);