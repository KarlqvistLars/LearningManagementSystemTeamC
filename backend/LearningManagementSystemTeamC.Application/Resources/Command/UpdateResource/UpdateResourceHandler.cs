using LearningManagementSystemTeamC.Application.ActivityResources;
using LearningManagementSystemTeamC.Application.Common.DTOs;
using LearningManagementSystemTeamC.Application.Common.Interfaces;
using LearningManagementSystemTeamC.Application.Common.Mappers;
using LearningManagementSystemTeamC.Domain.Common.Exceptions;
using LearningManagementSystemTeamC.Domain.Resources;

namespace LearningManagementSystemTeamC.Application.Resources.Command.UpdateResource;

public class UpdateResourceHandler : IUpdateResourceHandler
{
    private readonly IResourceRepository _resourceRepository;
    private readonly IUnitOfWork _unitOfWork;
    public UpdateResourceHandler(IResourceRepository resourceRepository, IUnitOfWork unitOfWork)
    {
        _resourceRepository = resourceRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<ResourceDto> Handle(
        UpdateResourceCommand command,
        CancellationToken cancellationToken)
    {
        var resource = await _resourceRepository.GetResourceByIdAsync(command.ResourceId, cancellationToken) ??
                        throw new NotFoundException(
                            ResourceRules.ResourceCode,
                            ResourceRules.ResourceNotFound);

        resource.Update(
            command.ResourceId,
            command.ResourceName,
            command.Content,
            command.Url,
            command.Type);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return ResourceMapper.ResourceToDto(resource);
    }
}