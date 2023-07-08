using EmpProjectManagement.Web.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace EmpProjectManagement.Web.Controllers;
public class ProjectController : Controller
{
    private readonly IProjectService _projectService;
    public ProjectController(IProjectService projectService)
    {
        _projectService = projectService;
    }
    public async Task<IActionResult> Index()
    {
        var data = await _projectService.GetProjects();
        return View(data);
    }
}
