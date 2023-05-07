using src.Models.TodoModel;
using src.Services.TodoServices;
using FakeItEasy;
using src.Models.ResModel;

namespace tests.TodoControllerTests;

public class TodoControllerTest
{
    public readonly ITodoService _todoService;
    public TodoControllerTest()
    {
        _todoService = A.Fake<ITodoService>();
    }
    [Fact]
    public void AddNewTodo_WithValidTodo_ReturnResTodo()
    {
        // Arrange
        Todo data = new(){Id = 1, Title = "clean", Context = "running 100 meeters", ProjectId = 1};
        Res<Todo> res = new() { Data =  data};
        TodoDto req = new (){ Title = "clean", Context = "running 100 meeters",  ProjectId = 1 };
        A.CallTo(() => _todoService.addNewTodo(req)).Returns(res);

        // Act
        var result = _todoService.addNewTodo(req);

        // Assert
        Assert.Null(result.Err);
        Assert.NotNull(result.Data);
        Assert.Equal(data, result.Data);
    }
    [Fact]
    public void UpdateTodo_WithValidTodo_ReturnResTodo()
    {
        // Arrange
        Todo data = new(){Id = 1, Title = "clean2", Context = "running 100 meeters2", ProjectId = 1};
        Res<Todo> res = new() { Data =  data};
        TodoDto req = new (){ Title = "clean2", Context = "running 100 meeters2",  ProjectId = 1 };
        A.CallTo(() => _todoService.updateTodo(1,req)).Returns(res);

        // Act
        var result = _todoService.updateTodo(1,req);

        // Assert
        Assert.Null(result.Err);
        Assert.NotNull(result.Data);
        Assert.Equal(data, result.Data);
    }
    [Fact]
    public void UpdateTodo_WithNotValidUserId_ReturnResStringErr()
    {
        // Arrange
        Res<Todo> res = new() { Err =  "User does not exist"};
        TodoDto req = new (){ Title = "clean2", Context = "running 100 meeters2",  ProjectId = 1 };
        A.CallTo(() => _todoService.updateTodo(99,req)).Returns(res);

        // Act
        var result = _todoService.updateTodo(99,req);

        // Assert
        Assert.Null(result.Data);
        Assert.NotNull(result.Err);
        Assert.Equal("User does not exist", result.Err);
    }
    [Fact]
    public void DeleteTodo_WithValidTodoId_ReturnResString()
    {
        // Arrange
        Res<string> res = new() { Data =  "Done"};
        A.CallTo(() => _todoService.deleteTodo(1)).Returns(res);

        // Act
        var result = _todoService.deleteTodo(1);

        // Assert
        Assert.Null(result.Err);
        Assert.NotNull(result.Data);
        Assert.Equal("Done", result.Data);
    }
    [Fact]
    public void DeleteTodo_WithNotValidTodoId_ReturnResStringErr()
    {
        // Arrange
        Res<string> res = new() { Err =  "Todo does not exist"};
        A.CallTo(() => _todoService.deleteTodo(99)).Returns(res);

        // Act
        var result = _todoService.deleteTodo(99);

        // Assert
        Assert.Null(result.Data);
        Assert.NotNull(result.Err);
        Assert.Equal("Todo does not exist", result.Err);
    }
}