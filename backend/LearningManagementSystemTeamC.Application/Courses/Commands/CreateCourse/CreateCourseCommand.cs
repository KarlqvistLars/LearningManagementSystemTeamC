namespace LearningManagementSystemTeamC.Application.Courses.Commands.CreateCourse;
    public record CreateCourseCommand(
    string CourseName,
    string Description,
    DateTime StartDate,
    DateTime EndDate);