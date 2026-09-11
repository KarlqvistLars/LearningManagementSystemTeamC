using LearningManagementSystemTeamC.Domain.ActivityResources;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LearningManagementSystemTeamC.Infrastructure.Persistence.Configurations;

internal class ActivityResourceConfiguration : IEntityTypeConfiguration<ActivityResource>
{
    public void Configure(EntityTypeBuilder<ActivityResource> builder)
    {
        builder.HasKey(ar => ar.Id);

        builder.Property(ar => ar.ResourceName)
            .IsRequired()
            .HasMaxLength(ActivityResourceRules.ResourceNameMaxLength);

        builder.Property(ar => ar.Content)
            .IsRequired()
            .HasMaxLength(ActivityResourceRules.ContentMaxLength);

        builder.Property(ar => ar.Url)
            .HasMaxLength(ActivityResourceRules.UrlMaxLength);

        builder.Property(ar => ar.CreatedAt)
            .IsRequired();

        builder.Property(ar => ar.Type)
            .IsRequired();

        builder.Property(ar => ar.UserId)
            .IsRequired();

        builder.Property(ar => ar.ActivityId)
            .IsRequired();
    }
}
