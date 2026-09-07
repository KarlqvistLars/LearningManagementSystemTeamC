using LearningManagementSystemTeamC.Api.Common.Constants;
using LearningManagementSystemTeamC.Api.Common.Contracts;
using LearningManagementSystemTeamC.Api.Controllers;
using LearningManagementSystemTeamC.Application.Common.DTOs;
using LearningManagementSystemTeamC.Application.Common.Interfaces;
using LearningManagementSystemTeamC.Application.Modules.Commands.CreateModule;
using LearningManagementSystemTeamC.Application.Modules.Queries.GetModuleById;
using LearningManagementSystemTeamC.Application.Modules.Queries.GetModules;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace LearchingManagementSystemTeamC.UnitTests;

public class ModulesControllerTests
{
    [Fact]
    public async Task Get_Module_By_Id_ReturnsOkResult()
    {
        // Arrange
        var moduleId = Guid.NewGuid();
        var courseId = Guid.NewGuid();

        var modules = new ModuleDto(
            moduleId,
            "Test Module",
            "A test description",
            DateTime.Parse("2025-05-01"),
            DateTime.Parse("2025-05-05"),
            courseId);
    

        var mockGetModuleByIdHandler = new Mock<IGetModuleByIdHandler>();

        mockGetModuleByIdHandler.Setup(x => x.Handle(It.Is<GetModuleByIdQuery>(q => q.Id == moduleId),
        It.IsAny<CancellationToken>())).ReturnsAsync(modules);

        var controller = new ModulesController();

        // Act

        var result = await controller.GetById(moduleId, mockGetModuleByIdHandler.Object,
        CancellationToken.None);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var response = Assert.IsType<ApiResponse<ModuleDto>>(okResult.Value);

        Assert.NotNull(response);
        Assert.Equal(modules, response.Data);

        mockGetModuleByIdHandler.Verify(x => x.Handle(It.Is<GetModuleByIdQuery>(q => q.Id == moduleId),
            It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Get_Module_By_Id_ReturnsNotFound()
    {
        // Arrange
        var moduleId = Guid.NewGuid();

        var mockGetModuleByIdHandler = new Mock<IGetModuleByIdHandler>();

        mockGetModuleByIdHandler.Setup(x => x.Handle(It.Is<GetModuleByIdQuery>(q => q.Id == moduleId),
        It.IsAny<CancellationToken>())).ReturnsAsync((ModuleDto?)null);

        var controller = new ModulesController();

        // Act

        var result = await controller.GetById(moduleId, mockGetModuleByIdHandler.Object,
        CancellationToken.None);

        // Assert
        var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
        var response = Assert.IsType<ApiResponse<ModuleDto>>(notFoundResult.Value);

        Assert.NotNull(response);
        Assert.NotNull(response.Error);
        Assert.Equal(ExceptionConstants.NotFoundCode, response.Error.Code);
        Assert.Equal(ExceptionConstants.NotFoundMessage, response.Error.Message);

        mockGetModuleByIdHandler.Verify(x => x.Handle(It.Is<GetModuleByIdQuery>(q => q.Id == moduleId),
            It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Get_Modules_By_Course_Id_ReturnsOkResult()
    {
        // Arrange

        var courseId = Guid.NewGuid();

        var modules = new List<ModuleDto>
        {
            new(
                Guid.NewGuid(),
                "Test Module",
                "A test description",
                DateTime.Parse("2025-05-01"),
                DateTime.Parse("2025-05-05"),
                courseId)             
        };

        var mockGetModulesHandler = new Mock<IGetModulesHandler>();

        mockGetModulesHandler.Setup(x => x.Handle(It.Is<GetModulesQuery>(q => q.CourseId == courseId),
        It.IsAny<CancellationToken>())).ReturnsAsync(modules);

        var controller = new ModulesController();

        // Act

        var result = await controller.GetModuleByCourseId(courseId, mockGetModulesHandler.Object,
        CancellationToken.None);

        // Assert

        var response = Assert.IsType<ApiResponse<IReadOnlyList<ModuleDto>>>(result.Value);

        Assert.NotNull(response);
        Assert.Equal(modules, response.Data);

        mockGetModulesHandler.Verify(x => x.Handle(It.Is<GetModulesQuery>(q => q.CourseId == courseId),
            It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Get_Modules_By_Course_Id_ReturnsNotFound()
    {
        // Arrange
        var courseId = Guid.NewGuid();

        var modules = new List<ModuleDto>();

        var mockGetModulesHandler = new Mock<IGetModulesHandler>();

        mockGetModulesHandler.Setup(x => x.Handle(It.Is<GetModulesQuery>(q => q.CourseId == courseId),
        It.IsAny<CancellationToken>())).ReturnsAsync(modules);

        var controller = new ModulesController();

        // Act

        var result = await controller.GetModuleByCourseId(courseId, mockGetModulesHandler.Object,
        CancellationToken.None);

        // Assert
        var notFoundResult = Assert.IsType<NotFoundObjectResult>(result.Result);

        var response = Assert.IsType<ApiResponse<ModuleDto>>(notFoundResult.Value);

        Assert.NotNull(response);
        Assert.NotNull(response.Error);

        Assert.Equal(ExceptionConstants.NotFoundCode, response.Error.Code);
        Assert.Equal(ExceptionConstants.NotFoundMessage, response.Error.Message);

        mockGetModulesHandler.Verify(x => x.Handle(It.Is<GetModulesQuery>(q => q.CourseId == courseId),
            It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Create_Module_ReturnsCreatedResult()
    {
        // Arrange
        var courseId = Guid.NewGuid();

        var command = new CreateModuleCommand(
            "Test Module",
            "Test Module Description",
            DateTime.Parse("2026-06-05"),
            DateTime.Parse("2026-06-10"),
            courseId);
        
        var moduleDto = new ModuleDto(
            Guid.NewGuid(),
            "Test Module",
            "Test Module Description",
            DateTime.Parse("2026-06-05"),
            DateTime.Parse("2026-06-10"),
            courseId);

        var mockCreateModuleHandler = new Mock<ICreateModuleHandler>();
        var mockValidator = new Mock<IValidator<CreateModuleCommand>>();

        mockValidator.Setup(x => x.Validate(command)).Returns(new Dictionary<string, string[]>());
        mockCreateModuleHandler.Setup(x => x.Handle(command, It.IsAny<CancellationToken>())).ReturnsAsync(moduleDto);

        var controller = new ModulesController();

        // Act
        var result = await controller.Create(command, mockCreateModuleHandler.Object, mockValidator.Object, CancellationToken.None);

        // Assert
        var createdResult = Assert.IsType<CreatedAtActionResult>(result);

        Assert.Equal(nameof(ModulesController.GetModuleByCourseId), createdResult.ActionName);
        Assert.Equal(courseId, createdResult.RouteValues!["courseId"]);

        var response = Assert.IsType<ApiResponse<ModuleDto>>(createdResult.Value);

        Assert.NotNull(response);
        Assert.Equal(moduleDto, response.Data);

        mockCreateModuleHandler.Verify(x => x.Handle(command, It.IsAny<CancellationToken>()), Times.Once);
        mockValidator.Verify(x => x.Validate(command), Times.Once);
    }

    [Fact]
    public async Task Create_Module_ReturnsBadRequest_WhenValidationFails()
    {
        // Arrange
        var courseId = Guid.NewGuid();

        var command = new CreateModuleCommand(
            "Test Module",
            "Test Module Description",
            DateTime.Parse("2026-06-05"),
            DateTime.Parse("2026-06-10"),
            courseId);

        var validationErrors = new Dictionary<string, string[]>
        {
            {
                "EndDate",new[] { "End date must be after start date." }
            }
        };

        var mockCreateModuleHandler = new Mock<ICreateModuleHandler>();
        var mockValidator = new Mock<IValidator<CreateModuleCommand>>();

        mockValidator.Setup(x => x.Validate(command)).Returns(validationErrors);

        var controller = new ModulesController();

        // Act
        var result = await controller.Create(command, mockCreateModuleHandler.Object, mockValidator.Object, CancellationToken.None);

        // Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);

        var response = Assert.IsType<ApiResponse<Dictionary<string, string[]>>>(badRequestResult.Value);

        Assert.NotNull(response);
        Assert.NotNull(response.Error);
        Assert.Equal(ExceptionConstants.ValidationFailedCode, response.Error.Code);
        Assert.Equal(ExceptionConstants.DefaultExceptionMessage,response.Error.Message);
        Assert.Equal(validationErrors, response.Error.Details);

        mockCreateModuleHandler.Verify(x => x.Handle(It.IsAny<CreateModuleCommand>(),It.IsAny<CancellationToken>()),Times.Never);
        mockValidator.Verify(x => x.Validate(command),Times.Once);
    }
}