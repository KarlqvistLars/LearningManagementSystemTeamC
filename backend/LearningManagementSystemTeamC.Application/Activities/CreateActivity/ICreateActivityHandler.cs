using LearningManagementSystemTeamC.Application.Common.DTOs;

namespace LearningManagementSystemTeamC.Application.Activities.CreateActivity;

public interface ICreateActivityHandler
{
    Task<ActivityDto> Handle(CreateActivityCommand command, CancellationToken cancellationToken);
}

