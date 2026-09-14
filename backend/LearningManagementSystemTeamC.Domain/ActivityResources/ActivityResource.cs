using LearningManagementSystemTeamC.Domain.Common.Exceptions;

namespace LearningManagementSystemTeamC.Domain.ActivityResources;

public class ActivityResource
{
    public Guid Id { get; set; }
    public Guid ActivityId { get; set; }
    public Guid ResourceId { get; set; }

    private ActivityResource() { }

    public ActivityResource(
        Guid activityId,
        Guid resourceId)
    {
        Validate(
            activityId,
            resourceId);

        Id = Guid.NewGuid();
        ActivityId = activityId;
        ResourceId = resourceId;
    }

    private static void Validate(
        Guid activityId,
        Guid resourceId)
    {
        if (activityId == Guid.Empty)
            throw new DomainException(
                ActivityResourceRules.ActivityIdRequiredCode,
                ActivityResourceRules.ActivityIdRequiredMessage);

        if (resourceId == Guid.Empty)
            throw new DomainException(
                ActivityResourceRules.ResourceIdRequiredCode,
                ActivityResourceRules.ResourceIdRequiredMessage);
    }
}