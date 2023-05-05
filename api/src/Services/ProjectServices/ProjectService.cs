using src.Helpers;
using src.Models.ProjectModel;
using src.Models.UserModel;
using src.Models.ResModel;
using src.Models.TodoModel;


namespace src.Services.ProjectServices;

public class ProjectService : IProjectService
{
    private readonly DataContext _context;
    private readonly IConfiguration _config;
    public ProjectService(IConfiguration config, DataContext context)
    {
        _context = context;
        _config = config;
    }

    public Res<Project> createNewProject(int userId, ProjectDto req)
    {
        Res<Project> response = new();
        User user = _context.Users?.Find(userId)!;
        if (user == null)
        {

            response.Err = "user not found";
            return response;
        }
        if (_context.Projects?.FirstOrDefault(proj => proj.Name == req.Name) != null)
        {
            response.Err = "project already exists";
            return response;

        }
        Project newProject = new Project { Name = req.Name, User = user, ShareUsersId = { user.Id }, ShareUsersUsername = { user.Username! } };
        _context.Projects?.Add(newProject);
        _context.SaveChanges();
        response.Data = newProject;
        return response;
    }

    public Res<string> addUserToProject(int userId, int projectId, string userEmail)
    {
        Res<string> response = new();
        User user = _context.Users?.Find(userId)!;
        User sharedUser = _context.Users?.FirstOrDefault(u => u.Email == userEmail)!;
        if (sharedUser == null)
        {
            response.Err = "couldn't find email";
            return response;
        }
        if (user != null)
        {
            Project project = _context.Projects?.Find(projectId)!;
            if (project != null)
            {
                if (project.UserId == user.Id)
                {
                    if (sharedUser != null)
                    {
                        // var p = project.ShareUsersId.FirstOrDefault(p => p.Id == sharedUser.Id);
                        if (!project.ShareUsersId.Any(p => p == sharedUser.Id))
                        {
                            project.ShareUsersId.Add(sharedUser.Id);
                            project.ShareUsersUsername.Add(sharedUser.Username!);
                            _context.Projects?.Update(project);
                            _context.SaveChanges();
                            response.Data = "Done";
                            return response;
                        }
                        response.Err = "id already exist";
                        return response;
                    }
                    else
                    {
                        response.Err = "the shared user does not exist";
                        return response;
                    }
                }
                else
                {
                    response.Err = "Project does not belongs to this user";
                    return response;
                }
            }
            else
            {
                response.Err = "Project does not exist";
                return response;
            }
        }
        response.Err = "User Does not exist";
        return response;
    }


    public Res<IQueryable<Project>> getAllUserProjects(int userId)
    {


        Res<IQueryable<Project>> res = new();
        if (_context.Users?.Find(userId) == null)
        {
            res.Err = "User does not exist";
            return res;
        }
        User? user = _context.Users.Find(userId);
        IQueryable<Project> allUserProjects = _context.Projects!.Where(p => p.ShareUsersId.Contains(userId));
        res.Data = allUserProjects;
        return res;
    }

    public Res<IQueryable<Todo>> getAllProjectsTodos(int userId, int projectId)
    {
        Res<IQueryable<Todo>> res = new();
        if (_context.Users?.Find(userId) == null)
        {
            res.Err = "User does not exist";
            return res;
        }

        IQueryable<Todo> allProjectTodos = _context.Todos!.Where(todo => todo.ProjectId == projectId);
        res.Data = allProjectTodos.OrderBy(t => t.index);
        return res;
    }

	public Res<string> deletePorject(int userId, int projectId)
    {

        Res<string> res = new();
        if (_context.Users?.Find(userId) == null)
        {
            res.Err = "User does not exist";
            return res;
        }
        Project project = _context.Projects?.Find(projectId)!;
        if ( project == null)
        {
            res.Err = "Project does not exist";
            return res;
        }

        _context.Projects!.Remove(project);
        _context.SaveChanges();
        return res;
    }
}

