using LearningManagementSystemTeamC.Application.Common.DTOs;

namespace LearningManagementSystemTeamC.Application.Activities.Command.CreateActivity;

public interface ICreateActivityHandler
{
    Task<ActivityDto> Handle(CreateActivityCommand command, CancellationToken cancellationToken);
}

