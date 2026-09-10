using LearningManagementSystemTeamC.Api.Common.Contracts;
using LearningManagementSystemTeamC.Api.Controllers;
using LearningManagementSystemTeamC.Application.Common.DTOs;
using LearningManagementSystemTeamC.Application.Common.Interfaces;
using LearningManagementSystemTeamC.Application.Courses.Commands.CreateCourse;
using LearningManagementSystemTeamC.Application.Courses.Queries.GetCourses;
using LearningManagementSystemTeamC.Application.Courses.Queries.GetCoursesByIdRange;
using LearningManagementSystemTeamC.Application.Enrollments.Queries.GetEnrollmentsByCourseId;
using LearningManagementSystemTeamC.Application.Enrollments.Queries.GetEnrollmentsByUserId;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace LearningManagementSystemTeamC.UnitTests.Courses;

public class CoursesControllerTests
{
    [Fact]
    public async Task Get_Courses_ReturnsOkResult()
    {
        // Arrange
        var mockGetCoursesHandler = new Mock<IGetCoursesHandler>();

        var courses = new List<CourseDto>
        {
            new(
                Guid.NewGuid(),
                "Test Course",
                "A description.",
                DateTime.Parse("2024-06-01"),
                DateTime.Parse("2024-06-30"))
        };

        mockGetCoursesHandler
            .Setup(handler => handler.Handle(It.IsAny<CancellationToken>()))
            .ReturnsAsync(courses);

        var controller = new CoursesController();

        // Act
        var result = await controller.GetAll(mockGetCoursesHandler.Object, CancellationToken.None);

        // Assert
        var okObjectResult = Assert.IsType<OkObjectResult>(result);
        var returnValue = Assert.IsType<ApiResponse<IEnumerable<CourseDto>>>(okObjectResult.Value);
        Assert.NotEmpty(returnValue.Data!);
    }

    [Fact]
    public async Task Get_EnrollmentsByCourse_ReturnsOkResult()
    {
        // Arrange
        var mockGetEnrollmentsByCourseIdHandler = new Mock<IGetEnrollmentsByCourseIdHandler>();
        var courseId = Guid.NewGuid();
        var enrollments = new List<CourseEnrollmentDto>
        {
            new(Guid.NewGuid(), DateTime.Now, "Student Name")
        };
        mockGetEnrollmentsByCourseIdHandler
            .Setup(handler => handler.Handle(It.IsAny<GetEnrollmentsByCourseIdQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(enrollments);
        var controller = new CoursesController();
        // Act
        var result = await controller.GetEnrollmentsByCourseId(courseId, mockGetEnrollmentsByCourseIdHandler.Object, CancellationToken.None);
        // Assert
        var okObjectResult = Assert.IsType<OkObjectResult>(result);
        var returnValue = Assert.IsType<ApiResponse<IEnumerable<CourseEnrollmentDto>>>(okObjectResult.Value);
        Assert.NotEmpty(returnValue.Data!);
    }

    [Fact]
    public async Task Get_CoursesByUserEnrollments_ReturnsOkResult()
    {
        // Arrange
        var mockGetEnrollmentsByUserIdHandler = new Mock<IGetEnrollmentsByUserIdHandler>();
        var mockGetCoursesByIdRangeHandler = new Mock<IGetCoursesByIdRangeHandler>();
        var userId = Guid.NewGuid();
        var course = new CourseDto(Guid.NewGuid(), "Test Course", "A description.", DateTime.Parse("2024-06-01"), DateTime.Parse("2024-06-30"));
        var enrollments = new List<EnrollmentDto>
        {
            new(userId, course.Id, DateTime.Now)
        };
        mockGetEnrollmentsByUserIdHandler
            .Setup(handler => handler.Handle(It.IsAny<GetEnrollmentsByUserIdQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(enrollments);
        mockGetCoursesByIdRangeHandler
            .Setup(handler => handler.Handle(It.IsAny<GetCoursesByIdRangeQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new List<CourseDto> { course });
        var controller = new CoursesController();
        
        // Act
        var result = await controller.GetCoursesByUserId(userId, mockGetEnrollmentsByUserIdHandler.Object, mockGetCoursesByIdRangeHandler.Object, CancellationToken.None);
        
        // Assert
        var okObjectResult = Assert.IsType<OkObjectResult>(result);
        var returnValue = Assert.IsType<ApiResponse<IEnumerable<CourseDto>>>(okObjectResult.Value);
        Assert.NotEmpty(returnValue.Data!);
    }
}
