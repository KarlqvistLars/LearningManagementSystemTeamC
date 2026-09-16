using LearningManagementSystemTeamC.Application.Common.DTOs;
using LearningManagementSystemTeamC.Application.Common.ReadModels;

namespace LearningManagementSystemTeamC.Application.Common.Mappers;

public static class AssignmentSubmissionMapper
{
    public static AssignmentSubmissionDto Map(
        AssignmentSubmissionReadModel readModel)
    {
        return new AssignmentSubmissionDto(
            readModel.StudentId,
            readModel.StudentFirstName,
            readModel.StudentLastName,
            readModel.ActivityId,
            readModel.ActivityName,
            readModel.EndDate,
            readModel.SubmissionId,
            readModel.SubmittedAt);
    }
}