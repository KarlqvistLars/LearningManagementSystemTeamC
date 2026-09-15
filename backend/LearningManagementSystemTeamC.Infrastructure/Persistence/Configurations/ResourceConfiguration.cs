using LearningManagementSystemTeamC.Domain.Resources;
using LearningManagementSystemTeamC.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LearningManagementSystemTeamC.Infrastructure.Persistence.Configurations;

internal class ResourceConfiguration : IEntityTypeConfiguration<Resource>
{
    public void Configure(EntityTypeBuilder<Resource> builder)
    {
        builder.HasKey(r => r.Id);

        builder.Property(r => r.ResourceName)
            .IsRequired()
            .HasMaxLength(ResourceRules.ResourceNameMaxLength);

        builder.Property(r => r.Content)
            .IsRequired()
            .HasMaxLength(ResourceRules.ContentMaxLength);

        builder.Property(r => r.Url)
            .HasMaxLength(ResourceRules.UrlMaxLength);

        builder.Property(r => r.CreatedDate)
            .IsRequired();

        builder.Property(r => r.Type)
            .IsRequired();

        builder.HasOne<User>()
            .WithMany()
            .HasForeignKey(r => r.CreatedBy)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
