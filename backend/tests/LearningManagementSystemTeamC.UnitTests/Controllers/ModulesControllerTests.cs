using LearningManagementSystemTeamC.Api.Common.Contracts;
using LearningManagementSystemTeamC.Api.Controllers;
using LearningManagementSystemTeamC.Application.Common.DTOs;
using LearningManagementSystemTeamC.Application.Modules.Queries.GetModule;
using Moq;

namespace LearchingManagementSystemTeamC.UnitTests;

public class ModulesControllerTests
{
    [Fact]
    public async Task Get_Modules_By_Id_ReturnsOkResult()
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

        var mockGetModulesHandler = new Mock<IGetModuleHandler>();

        mockGetModulesHandler.Setup(x => x.Handle(It.Is<GetModuleQuery>(q => q.CourseId == courseId),
        It.IsAny<CancellationToken>())).ReturnsAsync(modules);

        var controller = new ModulesController();

        // Act

        var result = await controller.GetModuleByCourseId(courseId, mockGetModulesHandler.Object,
        CancellationToken.None);

        // Assert

        var response = Assert.IsType<ApiResponse<IReadOnlyList<ModuleDto>>>(result.Value);

        Assert.NotNull(response);
        Assert.Equal(modules, response.Data);

        mockGetModulesHandler.Verify(x => x.Handle(It.Is<GetModuleQuery>(q => q.CourseId == courseId),
            It.IsAny<CancellationToken>()), Times.Once);

    }
}