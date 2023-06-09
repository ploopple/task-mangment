using Microsoft.AspNetCore.Mvc;
using src.Models.ProjectModel;
using src.Services.ProjectServices;
using Microsoft.AspNetCore.Authorization;
using src.Models.ResModel;
using src.Models.TodoModel;

namespace src.Controllers;

[ApiController, Authorize]
[Route("api/[controller]")]
public class ProjectController : ControllerBase
{
    private readonly IProjectService _projectService;

    public ProjectController(IProjectService projectService)
    {
        this._projectService = projectService;
    }

    [HttpPost("")]
    public ActionResult<Res<Project>> Post(  ProjectDto req)
    {
        int userId = int.Parse(User.Identity?.Name!);
        Res<Project> response = _projectService.createNewProject(userId, req);
        return response.Err == null ? response : BadRequest(response);
    }

    [HttpPost("addUserToProject")]
    public ActionResult<Res<string>> addUserToProject(int projectId, string sharedUserEmail)
    {
        int userId = int.Parse(User.Identity?.Name!);
        Res<string> response = _projectService.addUserToProject(userId, projectId, sharedUserEmail);
        return response.Err == null ? response : BadRequest(response);
    }



    [HttpGet("getAllUserProjects")]
    public ActionResult<Res<List<Project>>> getAllUserProjects()
    {
        int userId = int.Parse(User.Identity?.Name!);
        Res<List<Project>> response = _projectService.getAllUserProjects(userId);
        return response.Err == null ? response : BadRequest(response);
    }

    [HttpGet("getAllProjecTodo")]
    public ActionResult<Res<List<Todo>>> getAllProjecTodo(int projectId)
    {
        int userId = int.Parse(User.Identity?.Name!);
        Res<List<Todo>> response = _projectService.getAllProjectsTodos(userId,projectId);
        return response.Err == null ? response : BadRequest(response);
    }
    [HttpDelete("{projectId}")]
    public ActionResult<Res<string>> DeleteProject(int projectId)
    {
        int userId = int.Parse(User.Identity?.Name!);
        Res<string> response = _projectService.deletePorject(userId,projectId);
        return response.Err == null ? response : BadRequest(response);
    }

    [HttpPatch()]
    public ActionResult<Res<Project>> UpdateProject(int projectId, ProjectDto req)
    {
        int userId = int.Parse(User.Identity?.Name!);
        Res<Project> response = _projectService.updateProject(userId, projectId, req);
        return response.Err == null ? response : BadRequest(response);
    }
}