using LearningManagementSystemTeamC.Domain.UserInfos;
using LearningManagementSystemTeamC.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LearningManagementSystemTeamC.Infrastructure.Persistence.Configurations;

public class UserInfoConfiguration : IEntityTypeConfiguration<UserInfo>
{
    public void Configure(EntityTypeBuilder<UserInfo> builder)
    {
        builder.HasKey(x => x.UserId);

        builder.Property(x => x.UserId)
            .IsRequired();

        builder.Property(x => x.FirstName)
            .IsRequired()
            .HasMaxLength(UserInfoRules.FirstNameMaxLength);

        builder.Property(x => x.LastName)
            .IsRequired()
            .HasMaxLength(UserInfoRules.LastNameMaxLength);

        builder.Property(x => x.DateOfBirth)
            .IsRequired();

        builder.Property(x => x.PhoneNumber)
            .IsRequired()
            .HasMaxLength(UserInfoRules.PhoneNumberMaxLength);

        builder.Property(x => x.Address)
            .IsRequired()
            .HasMaxLength(UserInfoRules.AddressMaxLength);

        builder.Property(x => x.PostalCode)
            .IsRequired()
            .HasMaxLength(UserInfoRules.PostalCodeMaxLength);

        builder.Property(x => x.City)
            .IsRequired()
            .HasMaxLength(UserInfoRules.CityMaxLength);

        builder.HasOne<User>()
            .WithOne()
            .HasForeignKey<UserInfo>(x => x.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}