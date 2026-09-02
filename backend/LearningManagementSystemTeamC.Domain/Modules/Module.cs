using LearningManagementSystemTeamC.Domain.Courses;
using LearningManagementSystemTeamC.Domain.Common.Exceptions;
using LearningManagementSystemTeamC.Domain.Activities;

namespace LearningManagementSystemTeamC.Domain.Modules;

public class Module
{
    public Guid Id { get; set; }
    public string ModuleName { get; set; }
    public string Description { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public Guid CourseId { get; set; }
    public Course Course { get; private set; } = null!;
    public ICollection<Activity> Activities { get; private set; } = new List<Activity>();

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
                nameof(moduleName));

        if (string.IsNullOrWhiteSpace(description))
            throw new DomainException(
                ModuleRules.ModuleDescriptionRequiredCode,
                nameof(description));

        if (endDate <= startDate)
            throw new DomainException(
                ModuleRules.ModuleEndBeforeStartDateCode,
                nameof(endDate));
                
        if (courseId == Guid.Empty)
            throw new DomainException(
                ModuleRules.CourseIdRequiredMessage,
                nameof(courseId));
    }
}