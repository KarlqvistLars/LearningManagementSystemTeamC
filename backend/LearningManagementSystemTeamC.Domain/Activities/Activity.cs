using LearningManagementSystemTeamC.Domain.Common.Exceptions;

namespace LearningManagementSystemTeamC.Domain.Activities;

public class Activity
{
    public Guid Id { get; set; }
    public string ActivityName { get; set; }
    public string Description { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public ActivityType Type { get; set; }
    public Guid ModuleId { get; set; }

    public Activity(
        string activityName,
        string description,
        DateTime startDate,
        DateTime endDate,
        ActivityType type,
        Guid moduleId)
    {
        Validate(
            activityName,
            description,
            startDate,
            endDate,
            type,
            moduleId);

        Id = Guid.NewGuid();
        ActivityName = activityName;
        Description = description;
        StartDate = startDate;
        EndDate = endDate;
        Type = type;
        ModuleId = moduleId;
    }

    private static void Validate(
        string activityName,
        string description,
        DateTime startDate,
        DateTime endDate,
        ActivityType type,
        Guid moduleId)
    {
        if (string.IsNullOrWhiteSpace(activityName))
            throw new DomainException(
                nameof(activityName),
                ActivityRules.ActivityNameRequiredMessage
                );

        if (!Enum.IsDefined(typeof(ActivityType), type))
            throw new DomainException(
                nameof(type),
                ActivityRules.ActivityTypeRequiredMessage
                );

        if (string.IsNullOrWhiteSpace(description))
            throw new DomainException(
                nameof(description),
                ActivityRules.ActivityDescriptionRequiredMessage
                );

        if (endDate < startDate)
            throw new DomainException(
                nameof(endDate),
                ActivityRules.ActivityEndBeforeStartTimeMessage
                );

        if (moduleId == Guid.Empty)
            throw new DomainException(
                nameof(moduleId),
                ActivityRules.ModuleIdRequiredMessage
                );
    }
}
