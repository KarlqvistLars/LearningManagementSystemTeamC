using LearningManagementSystemTeamC.Application.ActivityResources;
using LearningManagementSystemTeamC.Application.Common.DTOs;
using LearningManagementSystemTeamC.Application.Common.Interfaces;
using LearningManagementSystemTeamC.Application.Common.Mappers;
using LearningManagementSystemTeamC.Domain.Common.Exceptions;
using LearningManagementSystemTeamC.Domain.Resources;
using LearningManagementSystemTeamC.Domain.Roles;

namespace LearningManagementSystemTeamC.Application.Resources.Command.UpdateResource;

public class UpdateResourceHandler : IUpdateResourceHandler
{
    private readonly IResourceRepository _resourceRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateResourceHandler(
        IResourceRepository resourceRepository,
        IUnitOfWork unitOfWork)
    {
        _resourceRepository = resourceRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<ResourceDto> HandleAsync(
        UpdateResourceCommand command,
        Guid userId,
        string roleCode,
        CancellationToken cancellationToken)
    {
        var resource =
            await _resourceRepository.GetResourceByIdAsync(
                command.ResourceId,
                cancellationToken)
            ?? throw new NotFoundException(
                ResourceRules.NotFoundCode,
                ResourceRules.NotFoundMessage);

        if (roleCode == RoleRules.StudentRoleCode)
        {
            if (resource.CreatedBy != userId)
            {
                throw new DomainException(
                    ResourceRules.ResourceUpdateNotAllowedCode,
                    ResourceRules.ResourceUpdateNotAllowedMessage);
            }

            if (resource.Type != ResourceType.Submission ||
                command.Type != ResourceType.Submission)
            {
                throw new DomainException(
                    ResourceRules.InvalidStudentResourceTypeCode,
                    ResourceRules.InvalidStudentResourceTypeMessage);
            }
        }

        resource.Update(
            command.ResourceName,
            command.Content,
            command.Url,
            command.Type);

        await _unitOfWork.SaveChangesAsync(
            cancellationToken);

        return ResourceMapper.ResourceToDto(resource);
    }
}