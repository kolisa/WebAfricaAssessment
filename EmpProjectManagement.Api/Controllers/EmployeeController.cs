using EmpProjectManagement.Models;
using EmpProjectManagement.Api.Repo.Employee;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmpProjectManagement.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class EmployeeController : ControllerBase
{
    private readonly IEmployee _employeeService;
    public EmployeeController(IEmployee employeeService)
    {
        _employeeService = employeeService;
    }
    [HttpGet]
    public async Task<ActionResult> GetAllAsyc()
    {
        var data = await _employeeService.GetAllAsync();
        return Ok(data);
    }
    [HttpPost]
    public async Task<ActionResult> AddAsync(AddEmployeeDto addEmployee)
    {
        var data = await _employeeService.AddAsync(addEmployee);
        if (!data.Success)
            return NoContent();
        return Ok(data);
    }
}
