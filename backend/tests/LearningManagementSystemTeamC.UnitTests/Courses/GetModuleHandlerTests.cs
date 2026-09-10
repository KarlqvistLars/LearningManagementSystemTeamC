using LearningManagementSystemTeamC.Application.Common.Interfaces;
using LearningManagementSystemTeamC.Application.Courses;
using LearningManagementSystemTeamC.Application.Enrollments;
using LearningManagementSystemTeamC.Application.Modules;
using LearningManagementSystemTeamC.Application.Modules.Queries.GetModule;
using LearningManagementSystemTeamC.Domain.Common.Exceptions;
using LearningManagementSystemTeamC.Domain.Courses;
using LearningManagementSystemTeamC.Domain.Enrollments;
using LearningManagementSystemTeamC.Domain.Modules;
using LearningManagementSystemTeamC.Domain.Roles;
using Moq;

namespace LearningManagementSystemTeamC.UnitTests.Courses;

public class GetModuleHandlerTests
{
    [Fact]
    public async Task Handle_StudentEnrolled_ReturnsModules()
    {
        // Arrange
        var courseId = Guid.NewGuid();
        var userId = Guid.NewGuid();

        var course = new Course(
            "Course Name",
            "Course Description",
            DateTime.Parse("2024-06-01"),
            DateTime.Parse("2024-06-02"));

        var modules = new List<Module>
        {
            new(
                "Module Name",
                "Module Description",
                DateTime.Parse("2024-06-01"),
                DateTime.Parse("2024-06-02"),
                courseId)
        };

        var courseRepository = new Mock<ICourseRepository>();
        var moduleRepository = new Mock<IModuleRepository>();
        var enrollmentRepository = new Mock<IEnrollmentRepository>();
        var unitOfWork = new Mock<IUnitOfWork>();

        courseRepository
            .Setup(repository => repository.GetByIdAsync(courseId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(course);

        enrollmentRepository
            .Setup(repository => repository.GetByUserIdAndCourseIdAsync(
                userId, courseId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(new Enrollment(userId, courseId));

        moduleRepository
            .Setup(repository => repository.GetModulesByCourseIdAsync(courseId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(modules);

        var handler = new GetModuleHandler(
            moduleRepository.Object,
            courseRepository.Object,
            enrollmentRepository.Object,
            unitOfWork.Object);

        var query = new GetModuleQuery(courseId, userId, RoleRules.StudentRoleCode);

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Single(result);
        Assert.Equal("Module Name", result[0].ModuleName);
    }

    [Fact]
    public async Task Handle_StudentNotEnrolled_ThrowsUnauthorizedException()
    {
        // Arrange
        var courseId = Guid.NewGuid();
        var userId = Guid.NewGuid();

        var course = new Course(
            "Course Name",
            "Course Description",
            DateTime.Parse("2024-06-01"),
            DateTime.Parse("2024-06-02"));

        var courseRepository = new Mock<ICourseRepository>();
        var moduleRepository = new Mock<IModuleRepository>();
        var enrollmentRepository = new Mock<IEnrollmentRepository>();
        var unitOfWork = new Mock<IUnitOfWork>();

        courseRepository
            .Setup(repository => repository.GetByIdAsync(courseId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(course);

        enrollmentRepository
            .Setup(repository => repository.GetByUserIdAndCourseIdAsync(
                userId, courseId, It.IsAny<CancellationToken>()))
            .ReturnsAsync((Enrollment?)null);

        var handler = new GetModuleHandler(
            moduleRepository.Object,
            courseRepository.Object,
            enrollmentRepository.Object,
            unitOfWork.Object);

        var query = new GetModuleQuery(courseId, userId, RoleRules.StudentRoleCode);

        // Act & Assert
        await Assert.ThrowsAsync<UnauthorizedException>(() =>
            handler.Handle(query, CancellationToken.None));

        moduleRepository.Verify(
            repository => repository.GetModulesByCourseIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()),
            Times.Never);
    }

    [Fact]
    public async Task Handle_Teacher_ReturnsModulesWithoutEnrollmentCheck()
    {
        // Arrange
        var courseId = Guid.NewGuid();
        var userId = Guid.NewGuid();

        var course = new Course(
            "Course Name",
            "Course Description",
            DateTime.Parse("2024-06-01"),
            DateTime.Parse("2024-06-02"));

        var modules = new List<Module>
        {
            new(
                "Module Name",
                "Module Description",
                DateTime.Parse("2024-06-01"),
                DateTime.Parse("2024-06-02"),
                courseId)
        };

        var courseRepository = new Mock<ICourseRepository>();
        var moduleRepository = new Mock<IModuleRepository>();
        var enrollmentRepository = new Mock<IEnrollmentRepository>();
        var unitOfWork = new Mock<IUnitOfWork>();

        courseRepository
            .Setup(repository => repository.GetByIdAsync(courseId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(course);

        moduleRepository
            .Setup(repository => repository.GetModulesByCourseIdAsync(courseId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(modules);

        var handler = new GetModuleHandler(
            moduleRepository.Object,
            courseRepository.Object,
            enrollmentRepository.Object,
            unitOfWork.Object);

        var query = new GetModuleQuery(courseId, userId, RoleRules.TeacherRoleCode);

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