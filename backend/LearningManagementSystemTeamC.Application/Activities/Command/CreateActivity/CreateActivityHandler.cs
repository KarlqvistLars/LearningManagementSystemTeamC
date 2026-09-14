using LearningManagementSystemTeamC.Application.Common.DTOs;
using LearningManagementSystemTeamC.Application.Common.Interfaces;
using LearningManagementSystemTeamC.Application.Common.Mappers;
using LearningManagementSystemTeamC.Domain.Activities;
using LearningManagementSystemTeamC.Domain.Resources;
namespace LearningManagementSystemTeamC.Application.Activities.Command.CreateActivity;

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
        var activity = new Activity(
            command.ActivityName,
            command.Description,
            command.StartDate,
            command.EndDate,
            command.Type,
            command.ModuleId) ?? throw new ArgumentNullException(
                ResourceRules.ActivityCreationFailed,
                ResourceRules.ActivityCreationFailedMessage
                );

        await _activityRepository.AddAsync(activity, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return ActivityMapper.ActivityToDto(activity);
    }
}
