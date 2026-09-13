using LearningManagementSystemTeamC.Application.ActivityResources;
using LearningManagementSystemTeamC.Application.ActivityResources.Command.CreateActivityResource;
using LearningManagementSystemTeamC.Application.Common.DTOs;
using LearningManagementSystemTeamC.Application.Common.Interfaces;
using LearningManagementSystemTeamC.Domain.Resources;

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

    public async Task<ResourceDto> Handle(
        CreateResourceCommand command,
        CancellationToken cancellationToken)
    {
        // Validation if not using other tools

        // Entity's method should have validation inside
        var resource = new Resource(
            command.ResourceName,
            command.Content,
            command.Url,
            command.CreatedAt,
            command.Type
            );
        // featureRepository handles actions
        await _resourceRepository.AddAsync(resource, cancellationToken);
        // UnitOfWork handles save
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return new ResourceDto(
            resource.Id,
            resource.ResourceName,
            resource.Content,
            resource.Url,
            resource.CreatedAt,
            resource.Type
        );
    }
}
