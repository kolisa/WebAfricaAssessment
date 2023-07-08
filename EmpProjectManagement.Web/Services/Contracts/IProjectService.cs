using EmpProjectManagement.Models;

namespace EmpProjectManagement.Web.Services.Contracts;

public interface IProjectService
{
    Task<List<ProjectDto>> GetProjects();
}
