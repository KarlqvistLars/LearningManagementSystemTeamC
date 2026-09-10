using LearningManagementSystemTeamC.Application.Common.DTOs;
using LearningManagementSystemTeamC.Application.Common.Interfaces;
namespace LearningManagementSystemTeamC.Application.Activities.CreateActivity;

public class CreateActivityHandler : ICreateActivityHandler
{
    private readonly IActivityRepository _activityRepository;

    private readonly IUnitOfWork _unitOfWork;

    public CreateActivityHandler(
        IActivityRepository activityRepository,
        IUnitOfWork unitOfWork)
    {
        _activityRepository = activityRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<ActivityDto> Handle(
        CreateActivityCommand command,
        CancellationToken cancellationToken)
    {
        // Validation if not using other tools

        // Entity's method should have validation inside
        var activity = new Domain.Activities.Activity(
            command.ActivityName,
            command.Description,
            command.StartDate,
            command.EndDate,
            command.Type,
            command.ModuleId);
        // featureRepository handles actions
        await _activityRepository.AddAsync(activity, cancellationToken);
        // UnitOfWork handles save
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return new ActivityDto(
            activity.Id,
            activity.ActivityName,
            activity.Description,
            activity.StartDate,
            activity.EndDate,
            activity.Type,
            activity.ModuleId
        );
    }
}
