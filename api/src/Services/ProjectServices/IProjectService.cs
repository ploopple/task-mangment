using src.Models.ProjectModel;
using src.Models.TodoModel;


using src.Models.ResModel;
namespace src.Services.ProjectServices
{
	public interface IProjectService
	{
		Res<Project> createNewProject(int userId, ProjectDto req);
		Res<string> addUserToProject(int userId, int projectId, string userEmail);
		Res<string> deletePorject(int userId, int projectId);
		Res<List<Project>> getAllUserProjects(int userId);
		Res<List<Todo>> getAllProjectsTodos(int userId, int projectId);
		Res<Project> updateProject(int userId, int projectId, ProjectDto req);
	}
}
