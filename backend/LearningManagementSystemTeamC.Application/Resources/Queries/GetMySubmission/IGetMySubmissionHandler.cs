using LearningManagementSystemTeamC.Application.Common.DTOs;

namespace LearningManagementSystemTeamC.Application.Resources.Queries.GetMySubmission;

public interface IGetMySubmissionHandler
{
    Task<ResourceDto?> HandleAsync(
        GetMySubmissionQuery query,
        CancellationToken cancellationToken);
}