using LearningManagementSystemTeamC.Application.Common.Interfaces;
using LearningManagementSystemTeamC.Domain.Courses;

namespace LearningManagementSystemTeamC.Application.Courses.Commands.CreateCourse;

public class CreateCourseValidator : IValidator<CreateCourseCommand>
{
    public Dictionary<string, string[]> Validate(
        CreateCourseCommand command)
    {
        var errors = new Dictionary<string, string[]>();

        if (string.IsNullOrWhiteSpace(command.Name))
        {
            errors[nameof(command.Name)] =
            [
                CourseRules.CourseNameRequiredMessage
            ];
        }
        else if (command.Name.Length > CourseRules.CourseNameMaxLength)
        {
            errors[nameof(command.Name)] =
            [
                CourseRules.CourseNameTooLongMessage
            ];
        }

        if (string.IsNullOrWhiteSpace(command.Description))
        {
            errors[nameof(command.Description)] =
            [
                CourseRules.DescriptionRequiredMessage
            ];
        }
        else if (command.Description.Length > CourseRules.DescriptionMaxLength)
        {
            errors[nameof(command.Description)] =
            [
                CourseRules.DescriptionTooLongMessage
            ];
        }

        if (command.EndDate <= command.StartDate)
        {
            errors[nameof(command.EndDate)] =
            [
                CourseRules.InvalidDatesMessage
            ];
        }

        return errors;
    }
}