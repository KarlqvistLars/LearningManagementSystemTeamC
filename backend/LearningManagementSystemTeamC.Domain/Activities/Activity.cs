using LearningManagementSystemTeamC.Domain.Common.Exceptions;
using LearningManagementSystemTeamC.Domain.Modules;

namespace LearningManagementSystemTeamC.Domain.Activities;

public class Activity
{
    public Guid Id { get; set; }
    public string ActivityName { get; set; }
    public ActivityType Type { get; set; }
    public string Description { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public Guid ModuleId { get; set; }
    public Module Module { get; private set; } = null!;

    public Activity(
        string activityName,
        ActivityType type,
        string description,
        DateTime startDate,
        DateTime endDate,
        Guid moduleId)
    {
        Validate(
            activityName,
            type,
            description,
            startDate,
            endDate,
            moduleId);

        Id = Guid.NewGuid();
        ActivityName = activityName;
        Type = type;
        Description = description;
        StartDate = startDate;
        EndDate = endDate;
        ModuleId = moduleId;
    }

    private static void Validate(
        string activityName,
        ActivityType type,
        string description,
        DateTime startDate,
        DateTime endDate,
        Guid moduleId)
    {
        if (string.IsNullOrWhiteSpace(activityName))
            throw new DomainException(
                ActivityRules.ActivityNameRequiredCode,
                nameof(activityName));

        if (!Enum.IsDefined(typeof(ActivityType), type))
            throw new DomainException(
                ActivityRules.ActivityTypeRequiredCode,
                nameof(type));

        if (string.IsNullOrWhiteSpace(description))
            throw new DomainException(
                ActivityRules.ActivityDescriptionRequiredCode,
                nameof(description));

        if (endDate <= startDate)
            throw new DomainException(
                ActivityRules.ActivityEndBeforeStartTimeCode,
                nameof(endDate));

        if (moduleId == Guid.Empty)
            throw new DomainException(
                ActivityRules.ModuleIdRequiredMessage,
                nameof(moduleId));
    }
}
