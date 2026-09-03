using LearningManagementSystemTeamC.Domain.Courses;
using LearningManagementSystemTeamC.Domain.Modules;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LearningManagementSystemTeamC.Infrastructure.Persistence.Configurations;

public class ModuleConfiguration : IEntityTypeConfiguration<Module>
{
    public void Configure(EntityTypeBuilder<Module> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.ModuleName)
            .IsRequired()
            .HasMaxLength(ModuleRules.ModuleNameMaxLength);
        
        builder.Property(x => x.Description)
            .IsRequired()
            .HasMaxLength(ModuleRules.DescriptionMaxLength);

        builder.Property(x => x.StartDate)
            .IsRequired();
        
        builder.Property(x => x.EndDate)
            .IsRequired();

        builder.HasOne<Course>()
            .WithMany()
            .HasForeignKey(x => x.CourseId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
        
    }
}