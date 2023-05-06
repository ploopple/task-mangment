using src.Models.UserModel;
using src.Services.ProjectServices;
using FakeItEasy;
using src.Models.ResModel;
using src.Models.ProjectModel;

namespace tests.UserControllerTests;

public class ProjectControllerTest
{
    public readonly IProjectService _projectService;
    public ProjectControllerTest()
    {
        _projectService = A.Fake<IProjectService>();
    }


    [Fact]
    public void CreateNewProject_WithValidProject_ReturnResProject()
    {
        // Arrange
        Res<Project> res = new() { Data = new(){Id = 1, Name = "project 1"} };
        ProjectDto req = new (){ Name = "project 1" };
        A.CallTo(() => _projectService.createNewProject(1,req)).Returns(res);

        // Act
        var result = _projectService.createNewProject(1,req);

        // Assert
        Assert.Null(result.Err);
        Assert.NotNull(result.Data);
        Assert.Equal(res.Data, result.Data);
    }
    [Fact]
    public void CreateNewProject_WithNotValidUserId_ReturnResErr()
    {
        // Arrange
        Res<Project> res = new() { Err = "User does not exist" };
        ProjectDto req = new (){ Name = "project 1" };
        A.CallTo(() => _projectService.createNewProject(99,req)).Returns(res);

        // Act
        var result = _projectService.createNewProject(99,req);

        // Assert
        Assert.Null(result.Data);
        Assert.NotNull(result.Err);
        Assert.Equal("User does not exist", result.Err);
    }
    [Fact]
    public void CreateNewProject_WithNotValidExistProjectName_ReturnResErr()
    {
        // Arrange
        Res<Project> res = new() { Err = "Project already exist" };
        ProjectDto req = new (){ Name = "project 1" };
        A.CallTo(() => _projectService.createNewProject(1,req)).Returns(res);

        // Act
        var result = _projectService.createNewProject(1,req);

        // Assert
        Assert.Null(result.Data);
        Assert.NotNull(result.Err);
        Assert.Equal("Project already exist", result.Err);
    }
}