using LearningManagementSystemTeamC.Domain.Courses;
using LearningManagementSystemTeamC.Domain.Common.Exceptions;

namespace LearningManagementSystemTeamC.Domain.Modules;

public class Module
{
    public Guid Id { get; private set; }
    public string ModuleName { get; private set; }
    public string Description { get; private set; }
    public DateTime StartDate { get; private set; }
    public DateTime EndDate { get; private set; }
    public Guid CourseId { get; private set; }

    public Module(
        string moduleName,
        string description,
        DateTime startDate,
        DateTime endDate,
        Guid courseId)
    {
        Validate(
            moduleName,
            description,
            startDate,
            endDate,
            courseId);

        Id = Guid.NewGuid();
        ModuleName = moduleName;
        Description = description;
        StartDate = startDate;
        EndDate = endDate;
        CourseId = courseId;
    }

    private static void Validate(
        string moduleName,
        string description,
        DateTime startDate,
        DateTime endDate,
        Guid courseId)
    {
        if (string.IsNullOrWhiteSpace(moduleName))
            throw new DomainException(
                ModuleRules.ModuleNameRequiredMessage,
                nameof(moduleName));

        if (string.IsNullOrWhiteSpace(description))
            throw new DomainException(
                ModuleRules.ModuleDescriptionRequiredMessage,
                nameof(description));

        if (endDate <= startDate)
            throw new DomainException(
                ModuleRules.ModuleEndBeforeStartDateMessage,
                nameof(endDate));
                
        if (courseId == Guid.Empty)
            throw new DomainException(
                ModuleRules.CourseIdRequiredMessage,
                nameof(courseId));
    }
}