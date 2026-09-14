using LearningManagementSystemTeamC.Domain.Activities;

namespace LearningManagementSystemTeamC.Domain.ActivityResources
{
    public class ActivityResource
    {
        public Guid Id { get; set; }
        public string ResourceName { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public string Url { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public ActivityType ResourceType { get; set; }
        public Guid UserId { get; set; }
        public Guid ActivityId { get; set; }

        public ActivityResource(
            string resourceName,
            string content,
            string url,
            ActivityType resourceType,
            Guid userId,
            Guid activityId)
        {
            Validate(
                resourceName,
                content,
                url,
                resourceType,
                userId,
                activityId);
            Id = Guid.NewGuid();
            ResourceName = resourceName;
            Content = content;
            Url = url;
            CreatedAt = DateTime.UtcNow;
            ResourceType = resourceType;
            UserId = userId;
            ActivityId = activityId;
        }

        private void Validate(
            string resourceName,
            string content,
            string url,
            ActivityType resourceType,
            Guid userId,
            Guid activityId)
        {
            if (string.IsNullOrWhiteSpace(resourceName))
            {
                throw new ArgumentException(
                    ActivityResourceRules.ResourceNameRequiredMessage,
                    nameof(resourceName));
            }
            if (string.IsNullOrWhiteSpace(content))
            {
                throw new ArgumentException(
                    ActivityResourceRules.ContentRequiredMessage,
                    nameof(content));
            }
            if (userId == Guid.Empty)
            {
                throw new ArgumentException(
                    ActivityResourceRules.UserIdRequiredMessage,
                    nameof(userId));
            }
            if (activityId == Guid.Empty)
            {
                throw new ArgumentException(
                    ActivityResourceRules.ActivityIdRequiredMessage,
                    nameof(activityId));
            }
        }
    }
}
