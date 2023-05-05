using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using src.Helpers;
using src.Models.TodoModel;
using src.Models.ProjectModel;
using src.Models.CommentModel;
using Microsoft.AspNetCore.Authorization;
using src.Services.TodoServices;
using src.Models.ResModel;

namespace src.Controllers;

[ApiController, Authorize]
[Route("api/[controller]")]
public class TodoController : ControllerBase {
    private readonly ITodoService _todoService;

    public TodoController(ITodoService todoService) {
        this._todoService = todoService;
    }

    [HttpPost]
    public ActionResult<Res<Todo>> Post( TodoDto req)
    {
        Res<Todo> response = _todoService.addNewTodo(req);
        return response.Err == null ? response : BadRequest(response);
    }

    [HttpPatch("{todoId}")]
    public  ActionResult<Res<Todo>> UpdateTaskPosition(int todoId,  TodoDto req)
    {
        Res<Todo> response = _todoService.updateTodo(todoId, req);
        return response.Err == null ? response : BadRequest(response);
    }

    [HttpDelete("{todoId}")]
    public ActionResult<Res<string>> Delete(int todoId)
    {
        Res<string> response = _todoService.deleteTodo(todoId);
        return response.Err == null ? response : BadRequest(response);
    }
}