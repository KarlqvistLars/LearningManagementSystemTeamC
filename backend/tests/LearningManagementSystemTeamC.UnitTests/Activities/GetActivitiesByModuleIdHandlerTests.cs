using LearningManagementSystemTeamC.Application.Activities;
using LearningManagementSystemTeamC.Application.Activities.Queries.GetActivitiesByModuleId;
using LearningManagementSystemTeamC.Application.Common.Interfaces;
using LearningManagementSystemTeamC.Application.Enrollments;
using LearningManagementSystemTeamC.Application.Modules;
using LearningManagementSystemTeamC.Domain.Activities;
using LearningManagementSystemTeamC.Domain.Common.Exceptions;
using LearningManagementSystemTeamC.Domain.Enrollments;
using LearningManagementSystemTeamC.Domain.Modules;
using LearningManagementSystemTeamC.Domain.Roles;
using Moq;

namespace LearningManagementSystemTeamC.UnitTests.Activities;

public class GetActivitiesByModuleIdHandlerTests
{
    [Fact]
    public async Task Handle_StudentEnrolled_ReturnsActivities()
    {
        // Arrange
        var moduleId = Guid.NewGuid();
        var courseId = Guid.NewGuid();
        var userId = Guid.NewGuid();

        var module = new Module(
            "Module Name",
            "Module Description",
            DateTime.Parse("2024-06-01"),
            DateTime.Parse("2024-06-02"),
            courseId);

        var activities = new List<Activity>
        {
            new(
                "Introduction to C#",
                ActivityType.Lecture,
                "Overview of the course.",
                DateTime.Parse("2024-06-01"),
                DateTime.Parse("2024-06-02"),
                moduleId)
        };

        var activityRepository = new Mock<IActivityRepository>();
        var moduleRepository = new Mock<IModuleRepository>();
        var enrollmentRepository = new Mock<IEnrollmentRepository>();
        var unitOfWork = new Mock<IUnitOfWork>();

        moduleRepository
            .Setup(repository => repository.GetByIdAsync(moduleId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(module);

        enrollmentRepository
            .Setup(repository => repository.GetByUserIdAndCourseIdAsync(
                userId, courseId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(new Enrollment(userId, courseId));

        activityRepository
            .Setup(repository => repository.GetActivitiesByModuleIdAsync(moduleId))
            .ReturnsAsync(activities);

        var handler = new GetActivitiesByModuleIdHandler(
            activityRepository.Object,
            moduleRepository.Object,
            enrollmentRepository.Object,
            unitOfWork.Object);

        var query = new GetActivitiesByModuleIdQuery(moduleId, userId, RoleRules.StudentRoleCode);

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Single(result);
        Assert.Equal("Introduction to C#", result[0].ActivityName);
    }

    [Fact]
    public async Task Handle_StudentNotEnrolled_ThrowsUnauthorizedException()
    {
        // Arrange
        var moduleId = Guid.NewGuid();
        var courseId = Guid.NewGuid();
        var userId = Guid.NewGuid();

        var module = new Module(
            "Module Name",
            "Module Description",
            DateTime.Parse("2024-06-01"),
            DateTime.Parse("2024-06-02"),
            courseId);

        var activityRepository = new Mock<IActivityRepository>();
        var moduleRepository = new Mock<IModuleRepository>();
        var enrollmentRepository = new Mock<IEnrollmentRepository>();
        var unitOfWork = new Mock<IUnitOfWork>();

        moduleRepository
            .Setup(repository => repository.GetByIdAsync(moduleId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(module);

        enrollmentRepository
            .Setup(repository => repository.GetByUserIdAndCourseIdAsync(
                userId, courseId, It.IsAny<CancellationToken>()))
            .ReturnsAsync((Enrollment?)null);

        var handler = new GetActivitiesByModuleIdHandler(
            activityRepository.Object,
            moduleRepository.Object,
            enrollmentRepository.Object,
            unitOfWork.Object);

        var query = new GetActivitiesByModuleIdQuery(moduleId, userId, RoleRules.StudentRoleCode);

        // Act & Assert
        await Assert.ThrowsAsync<UnauthorizedException>(() =>
            handler.Handle(query, CancellationToken.None));

        activityRepository.Verify(
            repository => repository.GetActivitiesByModuleIdAsync(It.IsAny<Guid>()),
            Times.Never);
    }

    [Fact]
    public async Task Handle_Teacher_ReturnsActivitiesWithoutEnrollmentCheck()
    {
        // Arrange
        var moduleId = Guid.NewGuid();
        var courseId = Guid.NewGuid();
        var userId = Guid.NewGuid();

        var module = new Module(
            "Module Name",
            "Module Description",
            DateTime.Parse("2024-06-01"),
            DateTime.Parse("2024-06-02"),
            courseId);

        var activities = new List<Activity>
        {
            new(
                "Introduction to C#",
                ActivityType.Lecture,
                "Overview of the course.",
                DateTime.Parse("2024-06-01"),
                DateTime.Parse("2024-06-02"),
                moduleId)
        };

        var activityRepository = new Mock<IActivityRepository>();
        var moduleRepository = new Mock<IModuleRepository>();
        var enrollmentRepository = new Mock<IEnrollmentRepository>();
        var unitOfWork = new Mock<IUnitOfWork>();

        moduleRepository
            .Setup(repository => repository.GetByIdAsync(moduleId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(module);

        activityRepository
            .Setup(repository => repository.GetActivitiesByModuleIdAsync(moduleId))
            .ReturnsAsync(activities);

        var handler = new GetActivitiesByModuleIdHandler(
            activityRepository.Object,
            moduleRepository.Object,
            enrollmentRepository.Object,
            unitOfWork.Object);

        var query = new GetActivitiesByModuleIdQuery(moduleId, userId, RoleRules.TeacherRoleCode);

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Single(result);

        enrollmentRepository.Verify(
            repository => repository.GetByUserIdAndCourseIdAsync(
                It.IsAny<Guid>(), It.IsAny<Guid>(), It.IsAny<CancellationToken>()),
            Times.Never);
    }
}