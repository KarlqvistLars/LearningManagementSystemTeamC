using LearningManagementSystemTeamC.Api.Common.Constants;
using LearningManagementSystemTeamC.Api.Common.Contracts;
using LearningManagementSystemTeamC.Api.Controllers;
using LearningManagementSystemTeamC.Application.Activities.CreateActivity;
using LearningManagementSystemTeamC.Application.Activities.Queries.GetActivitiesByModuleId;
using LearningManagementSystemTeamC.Application.Common.DTOs;
using LearningManagementSystemTeamC.Application.Common.Interfaces;
using LearningManagementSystemTeamC.Domain.Activities;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace LearningManagementSystemTeamC.UnitTests.Controllers;

public class ActivitiesControllerTests
{
    private readonly Mock<IUnitOfWork> _unitOfWorkMock = new();
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
                moduleId)
        };

        var mockHandler = new Mock<IGetActivitiesByModuleIdHandler>();
        mockHandler
            .Setup(handler => handler.Handle(
                It.IsAny<GetActivitiesByModuleIdQuery>(),
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

        var mockHandler = new Mock<IGetActivitiesByModuleIdHandler>();
        mockHandler
            .Setup(handler => handler.Handle(
                It.IsAny<GetActivitiesByModuleIdQuery>(),
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

    [Fact]
    public async Task Create_ReturnsBadRequestWhenValidationFails()
    {
        // Arrange
        var command = new CreateActivityCommand(
            "",
            "Description",
            new DateTime(2024, 6, 1),
            new DateTime(2024, 6, 2),
            ActivityType.Lecture,
            Guid.NewGuid());
        var mockHandler = new Mock<ICreateActivityHandler>();
        var mockValidator = new Mock<IValidator<CreateActivityCommand>>();
        mockValidator
            .Setup(validator => validator.Validate(
                command,
                It.IsAny<CancellationToken>()))
            .Returns(new Dictionary<string, string[]> {
                [nameof(CreateActivityCommand.ActivityName)] =
                    ["Activity name is required."]
            });
        var controller = new ActivitiesController();
        // Act
        var result = await controller.Create(command, mockHandler.Object, mockValidator.Object, CancellationToken.None);
        // Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        var returnValue = Assert.IsType<ApiResponse<ActivityDto>>(badRequestResult.Value);
        Assert.Equal(
            "Activity name is required.",
            returnValue.Error!.Details[
                nameof(CreateActivityCommand.ActivityName)
            ][0]);

        Assert.Equal(ExceptionConstants.ValidationFailedCode, returnValue.Error?.Code);
        Assert.Equal(ExceptionConstants.ValidationFailedMessage, returnValue.Error?.Message);
    }

    [Fact]
    public async Task Create_ReturnsCreatedAtActionResultWhenSuccessful()
    {
        // Arrange
        var command = new CreateActivityCommand(
            "Introduction to C#",
            "Overview of the course.",
            new DateTime(2024, 6, 1),
            new DateTime(2024, 6, 2),
            ActivityType.Lecture,
            Guid.NewGuid());
        var activityDto = new ActivityDto(
            Guid.NewGuid(),
            command.ActivityName,
            command.Description,
            command.StartDate,
            command.EndDate,
            command.Type,
            command.ModuleId);
        var mockHandler = new Mock<ICreateActivityHandler>();
        mockHandler
            .Setup(handler => handler.Handle(
                command,
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(activityDto);
        var mockValidator = new Mock<IValidator<CreateActivityCommand>>();
        mockValidator
            .Setup(validator => validator.Validate(
                command,
                It.IsAny<CancellationToken>()))
            .Returns(new Dictionary<string, string[]>());
        var controller = new ActivitiesController();
        // Act
        var result = await controller.Create(command, mockHandler.Object, mockValidator.Object, CancellationToken.None);
        // Assert
        var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
        var returnValue = Assert.IsType<ApiResponse<ActivityDto>>(createdAtActionResult.Value);
        Assert.Equal(activityDto.Id, returnValue.Data!.Id);
    }
}
