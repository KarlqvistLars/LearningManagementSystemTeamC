using LearningManagementSystemTeamC.Domain.Common.Exceptions;

namespace LearningManagementSystemTeamC.Domain.Resources;

public class Resource
{
    public Guid Id { get; private set; }
    public string ResourceName { get; private set; }
    public string Content { get; private set; }
    public string? Url { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public ResourceType Type { get; private set; }
    public Guid CreatedBy { get; private set; }

    public Resource(
        string resourceName,
        string content,
        string? url,
        DateTime createdAt,
        ResourceType type,
        Guid createdBy)
    {
        Validate(
            resourceName,
            content,
            url,
            createdAt,
            type,
            createdBy);

        Id = Guid.NewGuid();
        ResourceName = resourceName;
        Content = content;
        Url = url;
        CreatedAt = createdAt;
        Type = type;
        CreatedBy = createdBy;
    }

    public void Update(
        string resourceName,
        string content,
        string? url,
        ResourceType type)
    {
        Validate(
            resourceName,
            content,
            url,
            CreatedAt,
            type,
            CreatedBy);

        ResourceName = resourceName;
        Content = content;
        Url = url;
        Type = type;
    }

    private static void Validate(
        string resourceName,
        string content,
        string? url,
        DateTime createdAt,
        ResourceType type,
        Guid createdBy)
    {
        if (string.IsNullOrWhiteSpace(resourceName))
        {
            throw new DomainException(
                ResourceRules.ResourceNameRequiredCode,
                ResourceRules.ResourceNameRequiredMessage);
        }

        if (string.IsNullOrWhiteSpace(content))
        {
            throw new DomainException(
                ResourceRules.ContentRequiredCode,
                ResourceRules.ContentRequiredMessage);
        }

        if (createdAt == default)
        {
            throw new DomainException(
                ResourceRules.CreatedAtRequiredCode,
                ResourceRules.CreatedAtRequiredMessage);
        }

        if (!Enum.IsDefined(type))
        {
            throw new DomainException(
                ResourceRules.InvalidResourceTypeCode,
                ResourceRules.InvalidResourceTypeMessage);
        }

        if (createdBy == Guid.Empty)
        {
            throw new DomainException(
                ResourceRules.CreatedByRequiredCode,
                ResourceRules.CreatedByRequiredMessage);
        }

        if (!string.IsNullOrWhiteSpace(url) &&
            !Uri.TryCreate(url, UriKind.Absolute, out _))
        {
            throw new DomainException(
                ResourceRules.InvalidUrlCode,
                ResourceRules.InvalidUrlMessage);
        }
    }
}
