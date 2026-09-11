using LearningManagementSystemTeamC.Api.Common.Constants;
using LearningManagementSystemTeamC.Api.Common.Contracts;
using LearningManagementSystemTeamC.Api.Controllers;
using LearningManagementSystemTeamC.Application.Common.DTOs;
using LearningManagementSystemTeamC.Application.Common.Interfaces;
using LearningManagementSystemTeamC.Application.Modules.Commands.CreateModule;
using LearningManagementSystemTeamC.Application.Modules.Commands.EditModule;
using LearningManagementSystemTeamC.Application.Modules.Queries.GetModuleById;
using LearningManagementSystemTeamC.Application.Modules.Queries.GetModules;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace LearchingManagementSystemTeamC.UnitTests;

public class ModulesControllerTests
{
    private readonly Mock<IGetModuleByIdHandler> _mockGetModuleByIdHandler;
    private readonly Mock<IGetModulesHandler> _mockGetModulesHandler;
    private readonly Mock<ICreateModuleHandler> _mockCreateModuleHandler;
    private readonly Mock<IEditModuleHandler> _mockEditModuleHandler;
    private readonly ModulesController _controller;
    
    public ModulesControllerTests()
    {
        _mockGetModuleByIdHandler = new Mock<IGetModuleByIdHandler>();
        _mockGetModulesHandler = new Mock<IGetModulesHandler>();
        _mockCreateModuleHandler = new Mock<ICreateModuleHandler>();
        _mockEditModuleHandler = new Mock<IEditModuleHandler>();
        _controller = new ModulesController();
    }
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

        _mockGetModuleByIdHandler.Setup(x => x.Handle(It.Is<GetModuleByIdQuery>(q => q.Id == moduleId),
        It.IsAny<CancellationToken>())).ReturnsAsync(modules);

        // Act

        var result = await _controller.GetById(moduleId, _mockGetModuleByIdHandler.Object,
        CancellationToken.None);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var response = Assert.IsType<ApiResponse<ModuleDto>>(okResult.Value);

        Assert.NotNull(response);
        Assert.Equal(modules, response.Data);

        _mockGetModuleByIdHandler.Verify(x => x.Handle(It.Is<GetModuleByIdQuery>(q => q.Id == moduleId),
            It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Get_Module_By_Id_ReturnsNotFound()
    {
        // Arrange
        var moduleId = Guid.NewGuid();

        _mockGetModuleByIdHandler.Setup(x => x.Handle(It.Is<GetModuleByIdQuery>(q => q.Id == moduleId),
        It.IsAny<CancellationToken>())).ReturnsAsync((ModuleDto?)null);

        // Act

        var result = await _controller.GetById(moduleId, _mockGetModuleByIdHandler.Object,
        CancellationToken.None);

        // Assert
        var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
        var response = Assert.IsType<ApiResponse<ModuleDto>>(notFoundResult.Value);

        Assert.NotNull(response);
        Assert.NotNull(response.Error);
        Assert.Equal(ExceptionConstants.NotFoundCode, response.Error.Code);
        Assert.Equal(ExceptionConstants.NotFoundMessage, response.Error.Message);

        _mockGetModuleByIdHandler.Verify(x => x.Handle(It.Is<GetModuleByIdQuery>(q => q.Id == moduleId),
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

        _mockGetModulesHandler.Setup(x => x.Handle(It.Is<GetModulesQuery>(q => q.CourseId == courseId),
        It.IsAny<CancellationToken>())).ReturnsAsync(modules);

        // Act

        var result = await _controller.GetModuleByCourseId(courseId, _mockGetModulesHandler.Object,
        CancellationToken.None);

        // Assert

        var response = Assert.IsType<ApiResponse<IReadOnlyList<ModuleDto>>>(result.Value);

        Assert.NotNull(response);
        Assert.Equal(modules, response.Data);

        _mockGetModulesHandler.Verify(x => x.Handle(It.Is<GetModulesQuery>(q => q.CourseId == courseId),
            It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Get_Modules_By_Course_Id_ReturnsNotFound()
    {
        // Arrange
        var courseId = Guid.NewGuid();

        var modules = new List<ModuleDto>();

        _mockGetModulesHandler.Setup(x => x.Handle(It.Is<GetModulesQuery>(q => q.CourseId == courseId),
        It.IsAny<CancellationToken>())).ReturnsAsync(modules);

        // Act

        var result = await _controller.GetModuleByCourseId(courseId, _mockGetModulesHandler.Object,
        CancellationToken.None);

        // Assert
        var notFoundResult = Assert.IsType<NotFoundObjectResult>(result.Result);

        var response = Assert.IsType<ApiResponse<ModuleDto>>(notFoundResult.Value);

        Assert.NotNull(response);
        Assert.NotNull(response.Error);

        Assert.Equal(ExceptionConstants.NotFoundCode, response.Error.Code);
        Assert.Equal(ExceptionConstants.NotFoundMessage, response.Error.Message);

        _mockGetModulesHandler.Verify(x => x.Handle(It.Is<GetModulesQuery>(q => q.CourseId == courseId),
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

        var mockValidator = new Mock<IValidator<CreateModuleCommand>>();

        mockValidator.Setup(x => x.Validate(command)).Returns(new Dictionary<string, string[]>());
        _mockCreateModuleHandler.Setup(x => x.Handle(command, It.IsAny<CancellationToken>())).ReturnsAsync(moduleDto);

        // Act
        var result = await _controller.Create(command, _mockCreateModuleHandler.Object, mockValidator.Object, CancellationToken.None);

        // Assert
        var createdResult = Assert.IsType<CreatedAtActionResult>(result);

        Assert.Equal(nameof(ModulesController.GetModuleByCourseId), createdResult.ActionName);
        Assert.Equal(courseId, createdResult.RouteValues!["courseId"]);

        var response = Assert.IsType<ApiResponse<ModuleDto>>(createdResult.Value);

        Assert.NotNull(response);
        Assert.Equal(moduleDto, response.Data);

        _mockCreateModuleHandler.Verify(x => x.Handle(command, It.IsAny<CancellationToken>()), Times.Once);
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

        var mockValidator = new Mock<IValidator<CreateModuleCommand>>();

        mockValidator.Setup(x => x.Validate(command)).Returns(validationErrors);

        // Act
        var result = await _controller.Create(command, _mockCreateModuleHandler.Object, mockValidator.Object, CancellationToken.None);

        // Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);

        var response = Assert.IsType<ApiResponse<Dictionary<string, string[]>>>(badRequestResult.Value);

        Assert.NotNull(response);
        Assert.NotNull(response.Error);
        Assert.Equal(ExceptionConstants.ValidationFailedCode, response.Error.Code);
        Assert.Equal(ExceptionConstants.DefaultExceptionMessage,response.Error.Message);
        Assert.Equal(validationErrors, response.Error.Details);

        _mockCreateModuleHandler.Verify(x => x.Handle(It.IsAny<CreateModuleCommand>(),It.IsAny<CancellationToken>()),Times.Never);
        mockValidator.Verify(x => x.Validate(command),Times.Once);
    }

    [Fact]
    public async Task Edit_Module_ReturnsOkResult()
    {
        // Arrange
        var moduleId = Guid.NewGuid();
        var courseId = Guid.NewGuid();

        var moduleDto = new ModuleDto(
            moduleId,
            "Module",
            "Module Description",
            DateTime.Parse("2025-05-10"),
            DateTime.Parse("2025-10-05"),
            courseId);
        
        var command = new EditModuleCommand(
            moduleDto.Id,
            "New Module Name",
            "new Module Description",
            DateTime.Parse("2025-05-10"),
            DateTime.Parse("2025-10-05"),
            moduleDto.CourseId);
        
        var mockValidator = new Mock<IValidator<EditModuleCommand>>();

        mockValidator.Setup(x => x.Validate(command)).Returns(new Dictionary<string, string[]>());
        _mockEditModuleHandler.Setup(x => x.Handle(command, It.IsAny<CancellationToken>())).ReturnsAsync(moduleDto);

        // Act
        var result = await _controller.Edit(command, _mockEditModuleHandler.Object, mockValidator.Object, CancellationToken.None);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);

        var response = Assert.IsType<ApiResponse<ModuleDto>>(okResult.Value);

        Assert.NotNull(okResult.Value);
        Assert.NotNull(response.Data);
        Assert.Equal(moduleDto.Id, response.Data.Id);
        Assert.Equal(moduleDto.ModuleName, response.Data.ModuleName);
        Assert.Equal(moduleDto.Description, response.Data.Description);

        _mockEditModuleHandler.Verify(x => x.Handle(command, It.IsAny<CancellationToken>()), Times.Once);
       
    }

    [Fact]
    public async Task Edit_Module_ReturnsBadRequest()
    {
        // Arrange
        var moduleId = Guid.NewGuid();
        var courseId = Guid.NewGuid();

        
        var command = new EditModuleCommand(
            moduleId,
            "",
            "new Module Description",
            DateTime.Parse("2025-05-10"),
            DateTime.Parse("2025-10-05"),
            courseId);

        var validationErrors = new Dictionary<string, string[]>
        {
            {
                "Name", new[] { "Module name is required." }
            }  
        };
        
        var mockValidator = new Mock<IValidator<EditModuleCommand>>();

        mockValidator.Setup(x => x.Validate(command)).Returns(validationErrors);

        // Act
        var result = await _controller.Edit(command, _mockEditModuleHandler.Object, mockValidator.Object, CancellationToken.None);

        // Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);

        Assert.NotNull(badRequestResult.Value);

        _mockEditModuleHandler.Verify(x => x.Handle(command, It.IsAny<CancellationToken>()), Times.Never);
       
    }
}