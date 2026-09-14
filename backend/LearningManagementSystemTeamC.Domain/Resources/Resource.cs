namespace LearningManagementSystemTeamC.Domain.Resources
{
    public class Resource
    {
        public Guid Id { get; set; }
        public string ResourceName { get; set; }
        public string Content { get; set; }
        public string Url { get; set; }
        public DateTime CreatedAt { get; set; }
        public ResourceType Type { get; set; }

        public Resource(
            string resourceName,
            string content,
            string url,
            DateTime createdAt,
            ResourceType type)
        {
            Validate(
                resourceName,
                content,
                url,
                createdAt,
                type);

            Id = Guid.NewGuid();
            ResourceName = resourceName;
            Content = content;
            Url = url;
            CreatedAt = createdAt;
            Type = type;
        }

        public void Update(
            string resourceName,
            string content,
            string url,
            ResourceType type)
        {
            Validate(
                resourceName,
                content,
                url,
                CreatedAt,
                type);

            Id = resourceId;
            ResourceName = resourceName;
            Content = content;
            Url = url;
            Type = type;
        }

        private void Validate(
            string resourceName,
            string content,
            string url,
            DateTime createdAt,
            ResourceType type)
        {
            if (string.IsNullOrWhiteSpace(resourceName))
            {
                throw new ArgumentException(
                    ResourceRules.ResourceNameRequiredMessage,
                    nameof(resourceName));
            }
            if (string.IsNullOrWhiteSpace(content))
            {
                throw new ArgumentException(
                    ResourceRules.ContentRequiredMessage,
                    nameof(content));
            }
        }
    }
}
