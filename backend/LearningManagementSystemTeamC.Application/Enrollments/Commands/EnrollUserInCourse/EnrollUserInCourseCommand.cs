namespace LearningManagementSystemTeamC.Application.Enrollments.Commands.EnrollUserInCourse;

public record EnrollUserInCourseCommand(
    Guid UserId,
    Guid CourseId);