using LearningManagementSystemTeamC.Application.Common.DTOs;

namespace LearningManagementSystemTeamC.Application.Activities.Command.EditActivity;

public interface IEditActivityHandler
{
    Task<ActivityDto> Handle(EditActivityCommand command, CancellationToken cancellationToken);
}
