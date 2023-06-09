using src.Models.CommentModel;


using src.Models.ResModel;
namespace src.Services.CommentServices
{
	public interface ICommentService
	{
		Res<List<Comment>> getAllTodoComments(int todoId);
		Res<Comment> addNewComment(int userId, CommentDto req);
	}
}
