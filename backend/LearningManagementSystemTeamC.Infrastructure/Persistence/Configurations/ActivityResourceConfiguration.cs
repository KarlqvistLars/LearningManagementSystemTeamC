using LearningManagementSystemTeamC.Domain.ActivityResources;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LearningManagementSystemTeamC.Infrastructure.Persistence.Configurations;

internal class ActivityResourceConfiguration : IEntityTypeConfiguration<ActivityResource>
{
    public void Configure(EntityTypeBuilder<ActivityResource> builder)
    {
        builder.HasKey(ar => ar.Id);

        builder.Property(ar => ar.ActivityId)
            .IsRequired();

        builder.Property(ar => ar.ResourceId)
            .IsRequired();
    }
}
