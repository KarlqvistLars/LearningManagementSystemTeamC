namespace LearningManagementSystemTeamC.Application.Common.ReadModels;

public record AssignmentSubmissionReadModel(
    Guid StudentId,
    string StudentFirstName,
    string StudentLastName,
    Guid ActivityId,
    string ActivityName,
    DateTime EndDate,
    Guid? SubmissionId,
    DateTime? SubmittedAt);