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
                ModuleRules.ModuleNameRequiredCode,
                ModuleRules.ModuleNameRequiredMessage);

        if (string.IsNullOrWhiteSpace(description))
            throw new DomainException(
                ModuleRules.ModuleDescriptionRequiredCode,
                ModuleRules.ModuleDescriptionRequiredMessage);

        if (endDate <= startDate)
            throw new DomainException(
                ModuleRules.ModuleEndBeforeStartDateCode,
                ModuleRules.ModuleEndBeforeStartDateMessage);
                
        if (courseId == Guid.Empty)
            throw new DomainException(
                ModuleRules.CourseIdRequiredCode,
                ModuleRules.CourseIdRequiredMessage);
    }
}