using LearningManagementSystemTeamC.Domain.Messages;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LearningManagementSystemTeamC.Infrastructure.Persistence.Configurations;

public class MessageConfiguration : IEntityTypeConfiguration<Message>
{
    public void Configure(EntityTypeBuilder<Message> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Content)
            .IsRequired()
            .HasMaxLength(MessageRules.MessageContentMaxLength);

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.Property(x => x.ChatRoomId)
            .IsRequired();

        builder.Property(x => x.SenderId)
            .IsRequired();

        builder.HasIndex(x => new
        {
            x.ChatRoomId,
            x.CreatedAt
        });
    }
}