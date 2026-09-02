using LearningManagementSystemTeamC.Domain.Common.Exceptions;
using LearningManagementSystemTeamC.Domain.Modules;

namespace LearningManagementSystemTeamC.Domain.Activities;

public class Activity
{
    public Guid Id { get; set; }
    public string ActivityName { get; set; }
    public string ActivityType { get; set; }
    public string Description { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public Guid ModuleId { get; set; }
    public Module Module { get; private set; } = null!;

    public Activity(
        string activityName,
        string activityType,
        string description,
        DateTime startTime,
        DateTime endTime,
        Guid moduleId)
    {
        Validate(
            activityName,
            activityType,
            description,
            startTime,
            endTime,
            moduleId);

        Id = Guid.NewGuid();
        ActivityName = activityName;
        ActivityType = activityType;
        Description = description;
        StartTime = startTime;
        EndTime = endTime;
        ModuleId = moduleId;
    }

    private static void Validate(
        string activityName,
        string activityType,
        string description,
        DateTime startTime,
        DateTime endTime,
        Guid moduleId)
    {
        if (string.IsNullOrWhiteSpace(activityName))
            throw new DomainException(
                ActivityRules.ActivityNameRequiredCode,
                nameof(activityName));

        if (string.IsNullOrWhiteSpace(activityType))
            throw new DomainException(
                ActivityRules.ActivityTypeRequiredCode,
                nameof(activityType));

        if (string.IsNullOrWhiteSpace(description))
            throw new DomainException(
                ActivityRules.ActivityDescriptionRequiredCode,
                nameof(description));

        if (endTime <= startTime)
            throw new DomainException(
                ActivityRules.ActivityEndBeforeStartTimeCode,
                nameof(endTime));

        if (moduleId == Guid.Empty)
            throw new DomainException(
                ActivityRules.ModuleIdRequiredMessage,
                nameof(moduleId));
    }
}
