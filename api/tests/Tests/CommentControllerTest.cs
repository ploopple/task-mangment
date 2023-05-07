using src.Models.CommentModel;
using src.Services.CommentServices;
using FakeItEasy;
using src.Models.ResModel;

namespace tests.CommentControllerTests;

public class CommentControllerTest
{
    public readonly ICommentService _commentService;
    public CommentControllerTest()
    {
        _commentService = A.Fake<ICommentService>();
    }
    [Fact]
    public void AddNewComment_WithValidComment_ReturnResComment()
    {
        // Arrange
        Comment data = new(){Id = 1, UserId = 2,  Context = "running 100 meeters", TodoId = 1, UserName = "User2"};
        Res<Comment> res = new() { Data =  data};
        CommentDto req = new (){ UserId = 2,  Context = "running 100 meeters", TodoId = 1, UserName = "User2" };
        A.CallTo(() => _commentService.addNewComment(2, req)).Returns(res);

        // Act
        var result = _commentService.addNewComment(2,req);

        // Assert
        Assert.Null(result.Err);
        Assert.NotNull(result.Data);
        Assert.Equal(data, result.Data);
    }
    [Fact]
    public void AddNewComment_WithNotValidTodoId_ReturnResStringErr()
    {
        // Arrange
        Res<Comment> res = new() { Err =  "Todo does not exist"};
        CommentDto req = new (){ UserId = 2,  Context = "running 100 meeters", TodoId = 1, UserName = "User2" };
        A.CallTo(() => _commentService.addNewComment(99, req)).Returns(res);

        // Act
        var result = _commentService.addNewComment(99,req);

        // Assert
        Assert.Null(result.Data);
        Assert.NotNull(result.Err);
        Assert.Equal("Todo does not exist", result.Err);
    }
}