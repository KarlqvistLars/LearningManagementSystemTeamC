using LearningManagementSystemTeamC.Application.Common.DTOs;
using LearningManagementSystemTeamC.Application.Common.Interfaces;

namespace LearningManagementSystemTeamC.Application.ActivityResources.Command.CreateActivityResource;

public class CreateActivityResourceHandler : ICreateActivityResourceHandler
{
    private readonly IActivityResourceRepository _activityResourceRepository;

    private readonly IUnitOfWork _unitOfWork;

    public CreateActivityResourceHandler(
        IActivityResourceRepository activityResourceRepository,
        IUnitOfWork unitOfWork)
    {
        _activityResourceRepository = activityResourceRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<ActivityResourceDto> Handle(
        CreateActivityResourceCommand command,
        CancellationToken cancellationToken)
    {
        // Validation if not using other tools

        // Entity's method should have validation inside
        var activityResource = new Domain.ActivityResources.ActivityResource(
            command.ResourceName,
            command.Content,
            command.Url,
            command.Type,
            command.UserId,
            command.ActivityId);
        // featureRepository handles actions
        await _activityResourceRepository.AddAsync(activityResource, cancellationToken);
        // UnitOfWork handles save
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return new ActivityResourceDto(
            activityResource.Id,
            activityResource.ResourceName,
            activityResource.Content,
            activityResource.Url,
            activityResource.CreatedAt,
            activityResource.Type,
            activityResource.UserId,
            activityResource.ActivityId
        );
    }
}
