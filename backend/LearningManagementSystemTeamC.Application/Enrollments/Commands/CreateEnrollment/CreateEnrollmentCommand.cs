namespace LearningManagementSystemTeamC.Application.Enrollments.Commands.CreateEnrollment;

public record CreateEnrollmentCommand(
    Guid UserId,
    Guid CourseId,
    DateTime EnrolledAt,
    bool IsActive
);
