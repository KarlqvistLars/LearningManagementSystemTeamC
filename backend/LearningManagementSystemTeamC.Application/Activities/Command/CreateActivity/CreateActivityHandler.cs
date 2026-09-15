using LearningManagementSystemTeamC.Application.Common.DTOs;
using LearningManagementSystemTeamC.Application.Common.Interfaces;
using LearningManagementSystemTeamC.Application.Common.Mappers;
using LearningManagementSystemTeamC.Application.Modules;
using LearningManagementSystemTeamC.Domain.Activities;
using LearningManagementSystemTeamC.Domain.Common.Exceptions;
using LearningManagementSystemTeamC.Domain.Modules;
using LearningManagementSystemTeamC.Domain.Resources;
namespace LearningManagementSystemTeamC.Application.Activities.Command.CreateActivity;

public class CreateActivityHandler : ICreateActivityHandler
{
    private readonly IActivityRepository _activityRepository;

    private readonly IModuleRepository _moduleRepository;

    private readonly IUnitOfWork _unitOfWork;

    public CreateActivityHandler(
        IActivityRepository activityRepository,
        IModuleRepository moduleRepository,
        IUnitOfWork unitOfWork)
    {
        _activityRepository = activityRepository;
        _moduleRepository = moduleRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<ActivityDto> Handle(
        CreateActivityCommand command,
        CancellationToken cancellationToken)
    {
        var module = await _moduleRepository.GetByIdAsync(command.ModuleId, cancellationToken);
        if (module == null)
        {
            throw new NotFoundException(
                ModuleRules.ModuleNotFoundCode,
                ModuleRules.ModuleNotFoundMessage);
        }

        if (command.StartDate < module.StartDate || command.EndDate > module.EndDate)
        {
            throw new DomainException(
                ActivityRules.ActivityOutsideModuleCode,
                ActivityRules.ActivityOutsideModuleMessage);
        }

        var hasOverlap = await _activityRepository.HasOverlappingAsync(
            command.ModuleId,
            command.StartDate,
            command.EndDate,
            null,
            cancellationToken);
        if (hasOverlap)
        {
            throw new ConflictException(
                ActivityRules.ActivityOverlapCode,
                ActivityRules.ActivityOverlapMessage);
        }

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
