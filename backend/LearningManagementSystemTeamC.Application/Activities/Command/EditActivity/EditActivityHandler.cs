using LearningManagementSystemTeamC.Application.Activities.Command.CreateActivity;
using LearningManagementSystemTeamC.Application.Common.DTOs;
using LearningManagementSystemTeamC.Application.Common.Interfaces;
using LearningManagementSystemTeamC.Application.Common.Mappers;
using LearningManagementSystemTeamC.Application.Modules;
using LearningManagementSystemTeamC.Domain.Activities;
using LearningManagementSystemTeamC.Domain.Common.Exceptions;
using LearningManagementSystemTeamC.Domain.Modules;

namespace LearningManagementSystemTeamC.Application.Activities.Command.EditActivity;

public class EditActivityHandler : IEditActivityHandler
{
    private readonly IActivityRepository _activityRepository;
    private readonly IModuleRepository _moduleRepository;
    private readonly IUnitOfWork _unitOfWork;

    public EditActivityHandler(
        IActivityRepository activityRepository,
        IModuleRepository moduleRepository,
        IUnitOfWork unitOfWork)
    {
        _activityRepository = activityRepository;
        _moduleRepository = moduleRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<ActivityDto> Handle(
        EditActivityCommand command,
        CancellationToken cancellationToken)
    {
        var activity = await _activityRepository.GetByIdAsync(command.Id, cancellationToken);
        if (activity == null)
        {
            throw new NotFoundException(
                ActivityRules.ActivityNotFoundCode,
                ActivityRules.ActivityNotFoundMessage);
        }

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
            command.Id,
            cancellationToken);
        if (hasOverlap)
        {
            throw new ConflictException(
                ActivityRules.ActivityOverlapCode,
                ActivityRules.ActivityOverlapMessage);
        }

        activity.Update(
            command.ActivityName,
            command.Description,
            command.StartDate,
            command.EndDate,
            command.Type);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return ActivityMapper.ActivityToDto(activity);
    }
}
