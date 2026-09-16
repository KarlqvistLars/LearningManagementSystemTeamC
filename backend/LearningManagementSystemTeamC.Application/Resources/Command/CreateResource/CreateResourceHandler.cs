using LearningManagementSystemTeamC.Application.ActivityResources;
using LearningManagementSystemTeamC.Application.Common.DTOs;
using LearningManagementSystemTeamC.Application.Common.Interfaces;
using LearningManagementSystemTeamC.Domain.ActivityResources;
using LearningManagementSystemTeamC.Domain.Common.Exceptions;
using LearningManagementSystemTeamC.Domain.Resources;
using LearningManagementSystemTeamC.Domain.Roles;

namespace LearningManagementSystemTeamC.Application.Resources.Command.CreateResource;

public class CreateResourceHandler : ICreateResourceHandler
{
    private readonly IResourceRepository _resourceRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateResourceHandler(
        IResourceRepository resourceRepository,
        IUnitOfWork unitOfWork)
    {
        _resourceRepository = resourceRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<ResourceDto> HandleAsync(
        CreateResourceCommand command,
        Guid userId,
        string roleCode,
        CancellationToken cancellationToken)
    {
        if (roleCode == RoleRules.StudentRoleCode)
        {
            if (command.Type != ResourceType.Submission)
            {
                throw new DomainException(
                    ResourceRules.InvalidStudentResourceTypeCode,
                    ResourceRules.InvalidStudentResourceTypeMessage);
            }

            if (!command.ActivityId.HasValue ||
                command.ActivityId.Value == Guid.Empty)
            {
                throw new DomainException(
                    ResourceRules.ActivityIdRequiredCode,
                    ResourceRules.ActivityIdRequiredMessage);
            }
        }

        var resource = new Resource(
            command.ResourceName,
            command.Content,
            command.Url,
            command.Type,
            userId);

        await _resourceRepository.AddAsync(
            resource,
            cancellationToken);

        if (command.ActivityId.HasValue)
        {
            var activityResource = new ActivityResource(
                command.ActivityId.Value,
                resource.Id);

            await _resourceRepository.AddActivityResourceAsync(
                activityResource,
                cancellationToken);
        }

        await _unitOfWork.SaveChangesAsync(
            cancellationToken);

        return new ResourceDto(
            resource.Id,
            resource.ResourceName,
            resource.Content,
            resource.Url,
            resource.CreatedAt,
            resource.Type,
            resource.CreatedBy
        );
    }
}