using LearningManagementSystemTeamC.Api.Common.Contracts;
using LearningManagementSystemTeamC.Api.Controllers;
using LearningManagementSystemTeamC.Application.Activities.Queries.GetActivitiesByModule;
using LearningManagementSystemTeamC.Application.Common.DTOs;
using LearningManagementSystemTeamC.Domain.Activities;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace LearningManagementSystemTeamC.UnitTests.Controllers;

public class ActivitiesControllerTests
{
    [Fact]
    public async Task GetByModule_ReturnsOkResultWithActivities()
    {
        // Arrange
        var moduleId = Guid.NewGuid();

        var activities = new List<ActivityDto>
        {
            new(
                Guid.NewGuid(),
                "Introduction to C#",
                "Overview of the course.",
                DateTime.Parse("2024-06-01"),
                DateTime.Parse("2024-06-02"),
                ActivityType.Lecture,
                moduleId,
                "Programming Basics")
        };

        var mockHandler = new Mock<IGetActivitiesByModuleHandler>();
        mockHandler
            .Setup(handler => handler.Handle(
                It.IsAny<GetActivitiesByModuleQuery>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(activities);

        var controller = new ActivitiesController();

        // Act
        var result = await controller.GetByModule(moduleId, mockHandler.Object, CancellationToken.None);

        // Assert
        var okObjectResult = Assert.IsType<OkObjectResult>(result);
        var returnValue = Assert.IsType<ApiResponse<IReadOnlyList<ActivityDto>>>(okObjectResult.Value);
        Assert.NotEmpty(returnValue.Data!);
    }

    [Fact]
    public async Task GetByModule_ReturnsEmptyListWhenNoActivities()
    {
        // Arrange
        var moduleId = Guid.NewGuid();

        var mockHandler = new Mock<IGetActivitiesByModuleHandler>();
        mockHandler
            .Setup(handler => handler.Handle(
                It.IsAny<GetActivitiesByModuleQuery>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(new List<ActivityDto>());

        var controller = new ActivitiesController();

        // Act
        var result = await controller.GetByModule(moduleId, mockHandler.Object, CancellationToken.None);

        // Assert
        var okObjectResult = Assert.IsType<OkObjectResult>(result);
        var returnValue = Assert.IsType<ApiResponse<IReadOnlyList<ActivityDto>>>(okObjectResult.Value);
        Assert.Empty(returnValue.Data!);
    }
}
