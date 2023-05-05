using src.Models.CommentModel;


using src.Models.ResModel;
namespace src.Services.CommentServices
{
	public interface ICommentService
	{
		Res<IQueryable<Comment>> getAllTodoComments(int todoId);
		Res<Comment> addNewTodo(int userId, CommentDto req);
	}
}