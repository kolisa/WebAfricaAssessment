using EmpProjectManagement.Api.Repo.Project;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmpProjectManagement.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class ProjectController : ControllerBase
{
    private readonly IProject _projectService;
    public ProjectController(IProject projectService)
    {
        _projectService = projectService;
    }
    [HttpGet]
    public async Task<ActionResult> GetAllAsync()
    {
        var data = await _projectService.GetAllAsync();
        return Ok(data);
    }
}
