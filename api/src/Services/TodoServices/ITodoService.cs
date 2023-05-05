using src.Models.TodoModel;


using src.Models.ResModel;
namespace src.Services.TodoServices
{
	public interface ITodoService
	{
		Res<Todo> addNewTodo(TodoDto req);
		Res<Todo> updateTodo(int todoId, TodoDto req);
		Res<string> deleteTodo(int todoId);
	}
}
