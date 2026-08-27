namespace LearningManagementSystemTeamC.Application.Courses.Commands.CreateCourse;

public class CreateCourseValidator
{
    public bool IsValid(CreateCourseCommand command)
    {
        if (string.IsNullOrWhiteSpace(command.Name))
            return false;

        if (string.IsNullOrWhiteSpace(command.Description))
            return false;

        if (command.EndDate <= command.StartDate)
            return false;

        return true;
    }

    /*
    // OR: throw ValidationException and let
    // ExceptionMiddleware handle the API response.

    public void Validate(CreateCourseCommand command)
    {
        if (string.IsNullOrWhiteSpace(command.Name))
            throw new ValidationException("Name is required.");

        if (string.IsNullOrWhiteSpace(command.Description))
            throw new ValidationException("Description is required.");

        if (command.EndDate <= command.StartDate)
            throw new ValidationException(
                "End date must be after start date.");
    }
    */
}