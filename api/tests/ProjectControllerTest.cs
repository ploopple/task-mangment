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
    [Fact]
    public void AddUserToProject_WithValidProjectName_ReturnResString()
    {
        // Arrange
        Res<string> res = new() { Data = "Done" };
        A.CallTo(() => _projectService.addUserToProject(1,1, "random@gmail.com")).Returns(res);

        // Act
        var result = _projectService.addUserToProject(1,1 ,"random@gmail.com");

        // Assert
        Assert.Null(result.Err);
        Assert.NotNull(result.Data);
        Assert.Equal("Done", result.Data);
    }
    [Fact]
    public void AddUserToProject_WithNotValidUserId_ReturnResStringErr()
    {
        // Arrange
        Res<string> res = new() { Err = "User Does not exist" };
        A.CallTo(() => _projectService.addUserToProject(9,1, "random@gmail.com")).Returns(res);

        // Act
        var result = _projectService.addUserToProject(9,1 ,"random@gmail.com");

        // Assert
        Assert.Null(result.Data);
        Assert.NotNull(result.Err);
        Assert.Equal(res, result);
    }
    [Fact]
    public void AddUserToProject_WithNotValidProjectId_ReturnResStringErr()
    {
        // Arrange
        Res<string> res = new() { Err = "project Does not exist" };
        A.CallTo(() => _projectService.addUserToProject(1,999, "random@gmail.com")).Returns(res);

        // Act
        var result = _projectService.addUserToProject(1,999 ,"random@gmail.com");

        // Assert
        Assert.Null(result.Data);
        Assert.NotNull(result.Err);
        Assert.Equal("project Does not exist", result.Err);
    }
    [Fact]
    public void AddUserToProject_WithNotValidEmail_ReturnResStringErr()
    {
        // Arrange
        Res<string> res = new() { Err = "the shared user Does not exist" };
        A.CallTo(() => _projectService.addUserToProject(1,1, "void@gmail.com")).Returns(res);

        // Act
        var result = _projectService.addUserToProject(1,1 ,"void@gmail.com");

        // Assert
        Assert.Null(result.Data);
        Assert.NotNull(result.Err);
        Assert.Equal("the shared user Does not exist", result.Err);
    }
    [Fact]
    public void DeleteProject_WithValidProjectAndUserId_ReturnResString()
    {
        // Arrange
        Res<string> res = new() { Data = "Done" };
        A.CallTo(() => _projectService.deletePorject(1,1)).Returns(res);

        // Act
        var result = _projectService.deletePorject(1,1);

        // Assert
        Assert.Null(result.Err);
        Assert.NotNull(result.Data);
        Assert.Equal(res.Data, result.Data);
    }
    [Fact]
    public void DeleteProject_WithNotValidUserId_ReturnResStringErr()
    {
        // Arrange
        Res<string> res = new() { Err = "user Does not exist" };
        A.CallTo(() => _projectService.deletePorject(9,1)).Returns(res);

        // Act
        var result = _projectService.deletePorject(9,1);

        // Assert
        Assert.Null(result.Data);
        Assert.NotNull(result.Err);
        Assert.Equal("user Does not exist", result.Err);
    }
    [Fact]
    public void DeleteProject_WithNotProjectId_ReturnResStringErr()
    {
        // Arrange
        Res<string> res = new() { Err = "project Does not exist" };
        A.CallTo(() => _projectService.deletePorject(1,9)).Returns(res);

        // Act
        var result = _projectService.deletePorject(1,9);

        // Assert
        Assert.Null(result.Data);
        Assert.NotNull(result.Err);
        Assert.Equal("project Does not exist", result.Err);
    }
}