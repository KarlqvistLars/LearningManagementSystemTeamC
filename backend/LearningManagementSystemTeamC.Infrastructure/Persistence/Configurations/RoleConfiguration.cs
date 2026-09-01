using LearningManagementSystemTeamC.Domain.Roles;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LearningManagementSystemTeamC.Infrastructure.Persistence.Configurations;

public class RoleConfiguration : IEntityTypeConfiguration<Role>
{
    private readonly ApplicationDbContext _context;

    public RoleConfiguration(ApplicationDbContext context)
    {
        _context = context;
    }

    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name).IsRequired().HasMaxLength(RoleRules.NameMaxLength);
        builder.HasIndex(x => x.Name).IsUnique();

        builder.Property(x => x.Code).IsRequired().HasMaxLength(RoleRules.CodeMaxLength);
        builder.HasIndex(x => x.Code).IsUnique();
    }
}
