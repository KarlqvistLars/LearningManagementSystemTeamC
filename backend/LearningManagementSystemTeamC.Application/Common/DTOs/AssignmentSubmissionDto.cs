namespace LearningManagementSystemTeamC.Application.Common.DTOs;

public record AssignmentSubmissionDto(
    Guid StudentId,
    string StudentFirstName,
    string StudentLastName,
    Guid ActivityId,
    string ActivityName,
    DateTime EndDate,
    Guid? SubmissionId,
    DateTime? SubmittedAt);