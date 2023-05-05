using src.Helpers;
using src.Models.ProjectModel;
using src.Models.UserModel;
using src.Models.ResModel;
using src.Models.TodoModel;


namespace src.Services.TodoServices;

public class TodoService : ITodoService
{
    private readonly DataContext _context;
    private readonly IConfiguration _config;
    public TodoService(IConfiguration config, DataContext context)
    {
        _context = context;
        _config = config;
    }

    public Res<Todo> addNewTodo(TodoDto req)
    {
        Res<Todo> res = new();
        Project project = _context.Projects?.FirstOrDefault(u => u.Id == req.ProjectId)!;
        if (project == null)
        {
            res.Err = "project not found";
            return res;
        }
        Todo newTodo = new Todo { Title = req.Title, Project = project, username = req.username, index = req.index, Priority = req.Priority, AssignTo = req.AssignTo };
        _context.Todos?.Add(newTodo);
        _context.SaveChanges();
        res.Data = newTodo;
        return res;
    }
    public Res<Todo> updateTodo(int todoId, TodoDto req)
    {
        Res<Todo> response = new();
        Todo taskItem = _context.Todos?.Find(todoId)!;
        if (taskItem == null)
        {
            response.Err = "Todo does not exist";
            return response;
        }

        taskItem.Title = req.Title;
        taskItem.Context = req.Context!;
        taskItem.index = req.index;
        taskItem.Status = req.Status;
        taskItem.Priority = req.Priority;
        taskItem.AssignTo = req.AssignTo;
        _context.Todos?.Update(taskItem);
        _context.SaveChanges();

        response.Data = taskItem;
        return response;
    }
    public Res<string> deleteTodo(int todoId)
    {

        Res<string> response = new();
        Todo? Todo = _context.Todos?.Find(todoId);
        if (Todo == null)
        {
            response.Err = "Todo does not exisit";
            return response;
        }
        _context.Todos?.Remove(Todo);
        _context.SaveChanges();
        response.Data = "Todo deleted";
        return response;
    }
}

