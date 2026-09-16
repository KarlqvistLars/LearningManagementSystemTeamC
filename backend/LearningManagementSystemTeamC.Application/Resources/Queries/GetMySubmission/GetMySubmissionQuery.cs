namespace LearningManagementSystemTeamC.Application.Resources.Queries.GetMySubmission;

public record GetMySubmissionQuery(
    Guid ActivityId,
    Guid UserId);