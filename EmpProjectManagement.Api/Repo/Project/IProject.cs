using EmpProjectManagement.Models;
using EmpProjectManagement.Api.Util;

namespace EmpProjectManagement.Api.Repo.Project;

public interface IProject
{
    Task<List<ProjectDto>> GetAllAsync();
   
}
