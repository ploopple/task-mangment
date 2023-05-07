using src.Models.TodoModel;
using src.Services.ProjectServices;
using FakeItEasy;
using src.Models.ResModel;
using src.Models.ProjectModel;

namespace tests.ProjectControllerTests;

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
    [Fact]
    public void GetAllUserProjects_WithUserId_ReturnResListOfProjects()
    {
        // Arrange
        var data =(List<Project>) new List<Project>() {new Project {Id = 1, Name = "proj 1"}, new Project {Id= 2,Name = "proj 2"}};
        Res<List<Project>> res = new () {Data = data};
        A.CallTo(() => _projectService.getAllUserProjects(1)).Returns(res);

        // Act
        var result = _projectService.getAllUserProjects(1);

        // Assert
        Assert.Null(result.Err);
        Assert.NotNull(result.Data);
        Assert.Equal(data, result.Data);
    }
    [Fact]
    public void GetAllUserProjects_WithNotUserId_ReturnResStringErr()
    {
        // Arrange
        Res<List<Project>> res = new () { Err = "User does not exist"};
        A.CallTo(() => _projectService.getAllUserProjects(99)).Returns(res);

        // Act
        var result = _projectService.getAllUserProjects(99);

        // Assert
        Assert.Null(result.Data);
        Assert.NotNull(result.Err);
        Assert.Equal("User does not exist", result.Err);
    }
    [Fact]
    public void GetAllProjectTodos_WithValidUserIdAndProjectId_ReturnResListOfTodos()
    {
        // Arrange
        var data =(List<Todo>) new List<Todo>() {new Todo {Id = 1, Title = "clean"}, new Todo {Id= 2,Title = "run"}};
        Res<List<Todo>> res = new () {Data = data};
        A.CallTo(() => _projectService.getAllProjectsTodos(1, 1)).Returns(res);

        // Act
        var result = _projectService.getAllProjectsTodos(1,1);

        // Assert
        Assert.Null(result.Err);
        Assert.NotNull(result.Data);
        Assert.Equal(data, result.Data);
    }
    [Fact]
    public void GetAllProjectTodos_WithNotValidUserId_ReturnResStringErr()
    {
        // Arrange
        Res<List<Todo>> res = new () { Err = "User does not exist"};
        A.CallTo(() => _projectService.getAllProjectsTodos(99,1)).Returns(res);

        // Act
        var result = _projectService.getAllProjectsTodos(99, 1);

        // Assert
        Assert.Null(result.Data);
        Assert.NotNull(result.Err);
        Assert.Equal("User does not exist", result.Err);
    }
    [Fact]
    public void GetAllProjectTodos_WithNotValidProjectId_ReturnResStringErr()
    {
        // Arrange
        Res<List<Todo>> res = new () { Err = "Project does not exist"};
        A.CallTo(() => _projectService.getAllProjectsTodos(1, 99)).Returns(res);

        // Act
        var result = _projectService.getAllProjectsTodos(1, 99);

        // Assert
        Assert.Null(result.Data);
        Assert.NotNull(result.Err);
        Assert.Equal("Project does not exist", result.Err);
    }
    [Fact]
    public void UpdateProject_WithValidUserIdAndProjectId_ReturnResProject()
    {
        // Arrange
        Project data = new () {Id = 1, Name = "proj 1"};
        Res<Project> res = new () {Data = data};
        ProjectDto req = new () {Name = "proj 1"};
        A.CallTo(() => _projectService.updateProject(1, 1, req)).Returns(res);

        // Act
        var result = _projectService.updateProject(1,1, req);

        // Assert
        Assert.Null(result.Err);
        Assert.NotNull(result.Data);
        Assert.Equal(data, result.Data);
    }
    [Fact]
    public void GetAllProjectTodos_WithValidUserId_ReturnResStringErr()
    {
        // Arrange
        Res<Project> res = new () { Err = "User does not exist"};
        ProjectDto req = new () {Name = "proj 1"};
        A.CallTo(() => _projectService.updateProject(99,1, req)).Returns(res);

        // Act
        var result = _projectService.updateProject(99, 1, req);

        // Assert
        Assert.Null(result.Data);
        Assert.NotNull(result.Err);
        Assert.Equal("User does not exist", result.Err);
    }
    [Fact]
    public void GetAllProjectTodos_WithValidProjectId_ReturnResStringErr()
    {
        // Arrange
        Res<Project> res = new () { Err = "Project does not exist"};
        ProjectDto req = new () {Name = "proj 1"};
        A.CallTo(() => _projectService.updateProject(1,99, req)).Returns(res);

        // Act
        var result = _projectService.updateProject(1, 99, req);

        // Assert
        Assert.Null(result.Data);
        Assert.NotNull(result.Err);
        Assert.Equal("Project does not exist", result.Err);
    }
}