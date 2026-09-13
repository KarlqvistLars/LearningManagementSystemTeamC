namespace LearningManagementSystemTeamC.Application.Enrollments.Commands.EnrollUserInCourse;

public interface IEnrollUserInCourseHandler
{
    Task<bool> Handle(EnrollUserInCourseCommand command, CancellationToken cancellationToken);
}