namespace LearningManagementSystemTeamC.Application.Courses.Commands.CreateCourse;

public interface ICreateCourseValidator
{
    Dictionary<string, string[]> Validate(CreateCourseCommand command);
}
