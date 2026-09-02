using LearningManagementSystemTeamC.Api.Common.Contracts;
using LearningManagementSystemTeamC.Api.Controllers;
using LearningManagementSystemTeamC.Application.Common.DTOs;
using LearningManagementSystemTeamC.Application.Common.Interfaces;
using LearningManagementSystemTeamC.Application.Courses.Commands.CreateCourse;
using LearningManagementSystemTeamC.Application.Courses.Commands.GetCourse;
using LearningManagementSystemTeamC.Application.Courses.Queries.GetCourses;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace LearningManagementSystemTeamC.UnitTests;

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
}
