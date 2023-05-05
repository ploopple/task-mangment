using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using src.Helpers;
using src.Models.CommentModel;
using src.Models.TodoModel;
using src.Models.ResModel;
using Microsoft.AspNetCore.Authorization;
using src.Services.CommentServices;

namespace src.Controllers;

[ApiController, Authorize]
[Route("api/[controller]")]
public class CommentController : ControllerBase {
    private readonly ICommentService _commentService;

    public CommentController(ICommentService commentService) {
        this._commentService = commentService;
    }


    [HttpPost]
    public ActionResult<Res<Comment>> Post( CommentDto req)
    {
        int userId = int.Parse(User.Identity?.Name!);
        Res<Comment> response = _commentService.addNewTodo(userId,req) ;
        return response.Err == null ? response : BadRequest(response); 
    }

    [HttpGet]
    public ActionResult<Res<IQueryable<Comment>>> GetAllCommentsTodo(int todoId) {
        Res<IQueryable<Comment>> response = _commentService.getAllTodoComments(todoId);
        return response.Err == null ? response : BadRequest(response); 
    }
}