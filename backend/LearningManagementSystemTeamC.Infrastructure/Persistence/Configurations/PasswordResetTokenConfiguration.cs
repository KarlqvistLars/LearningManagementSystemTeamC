using LearningManagementSystemTeamC.Application.Auth.Commands.ForgotPassword;
using LearningManagementSystemTeamC.Domain.PasswordResetTokens;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LearningManagementSystemTeamC.Infrastructure.Persistence.Configurations;

public class PasswordResetTokenConfiguration : IEntityTypeConfiguration<PasswordResetToken>
{
    public void Configure(EntityTypeBuilder<PasswordResetToken> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.UserId)
            .IsRequired();

        builder.Property(x => x.TokenHash)
            .IsRequired()
            .HasMaxLength(ForgotPasswordRules.TokenHashMaxSize);

        builder.Property(x => x.ExpiresAt)
            .IsRequired();

        builder.Property(x => x.UsedAt);

        builder.HasIndex(x => x.TokenHash)
            .IsUnique();

        builder.HasIndex(x => x.UserId);
    }
}