using LearningManagementSystemTeamC.Domain.Resources;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LearningManagementSystemTeamC.Infrastructure.Persistence.Configurations;

internal class ResourceConfiguration : IEntityTypeConfiguration<Resource>
{
    public void Configure(EntityTypeBuilder<Resource> builder)
    {
        builder.HasKey(ar => ar.Id);

        builder.Property(ar => ar.ResourceName)
            .IsRequired()
            .HasMaxLength(ResourceRules.ResourceNameMaxLength);

        builder.Property(ar => ar.Content)
            .IsRequired()
            .HasMaxLength(ResourceRules.ContentMaxLength);

        builder.Property(ar => ar.Url)
            .HasMaxLength(ResourceRules.UrlMaxLength);

        builder.Property(ar => ar.CreatedAt)
            .IsRequired();

        builder.Property(ar => ar.Type)
            .IsRequired();
    }
}
