using LearningManagementSystemTeamC.Domain.Activities;
using LearningManagementSystemTeamC.Domain.ChatRoomMembers;
using LearningManagementSystemTeamC.Domain.ChatRooms;
using LearningManagementSystemTeamC.Domain.Courses;
using LearningManagementSystemTeamC.Domain.Enrollments;
using LearningManagementSystemTeamC.Domain.Messages;
using LearningManagementSystemTeamC.Domain.Modules;
using LearningManagementSystemTeamC.Domain.PasswordResetTokens;
using LearningManagementSystemTeamC.Domain.Roles;
using LearningManagementSystemTeamC.Domain.UserInfos;
using LearningManagementSystemTeamC.Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace LearningManagementSystemTeamC.Infrastructure.Persistence;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(
        DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Course> Courses => Set<Course>();
    public DbSet<User> Users => Set<User>();
    public DbSet<UserInfo> UserInfos => Set<UserInfo>();
    public DbSet<Role> Roles => Set<Role>();
    public DbSet<Module> Modules => Set<Module>();
    public DbSet<Enrollment> Enrollments => Set<Enrollment>();
    public DbSet<Activity> Activities => Set<Activity>();
    public DbSet<PasswordResetToken> PasswordResetTokens => Set<PasswordResetToken>();
    public DbSet<ChatRoom> ChatRooms => Set<ChatRoom>();
    public DbSet<ChatRoomMember> ChatRoomMembers => Set<ChatRoomMember>();
    public DbSet<Message> Messages => Set<Message>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(ApplicationDbContext).Assembly);

        base.OnModelCreating(modelBuilder);
    }
}