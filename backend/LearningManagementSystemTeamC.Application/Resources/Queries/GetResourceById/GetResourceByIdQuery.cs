namespace LearningManagementSystemTeamC.Application.Resources.Queries.GetResourceById
{
    public class GetResourceByIdQuery
    {
        public Guid ResourceId { get; }

        public GetResourceByIdQuery(Guid resourceId)
        {
            ResourceId = resourceId;
        }
    }
}
