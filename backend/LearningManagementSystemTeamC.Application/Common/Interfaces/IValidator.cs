namespace LearningManagementSystemTeamC.Application.Common.Interfaces;

public interface IValidator<in T>
{
    Dictionary<string, string[]> Validate(T command);
}
