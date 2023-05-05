using src.Helpers;
using src.Models.TodoModel;
using src.Models.UserModel;
using src.Models.ResModel;
using src.Models.CommentModel;


namespace src.Services.CommentServices;

public class CommentService : ICommentService 
{
    private readonly DataContext _context;
    private readonly IConfiguration _config;
    public CommentService(IConfiguration config, DataContext context)
    {
        _context = context;
        _config = config;
    }

    public Res<IQueryable<Comment>> getAllTodoComments(int todoId)
    {
        Res<IQueryable<Comment>> response = new();
Todo? Todo = _context.Todos?.Find(todoId);
        if (Todo == null)
        {
            response.Err = "Todo does not exisit";
            return response;
        } 
        response.Data = _context.Comments?.Where(com => com.TodoId == todoId);
        return response;
    }
    public Res<Comment> addNewTodo(int userId, CommentDto req)
    {
        Res<Comment> response = new();
        Todo? todo = _context.Todos?.FirstOrDefault(u => u.Id == req.TodoId);
        if (todo == null)
        {
            response.Err = "todo not found";
            return response;
        }
        Comment newComment = new Comment { Context = req.Context, UserId = userId, TodoId = req.TodoId, UserName = req.UserName };
        _context.Comments?.Add(newComment);
        _context.SaveChanges();
        response.Data = newComment;
        return response;
    }
}

