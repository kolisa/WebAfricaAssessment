using EmpProjectManagement.Models;
using EmpProjectManagement.Web.Models;
using EmpProjectManagement.Web.Services.Contracts;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace EmpProjectManagement.Web.Controllers;
public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IEmployeeService _employeeService;
    public HomeController(ILogger<HomeController> logger, IEmployeeService employeeService)
    {
        _logger = logger;
        _employeeService = employeeService;
    }

    public async Task<IActionResult> Index()
    {
        var data = await _employeeService.GetEmployees();
        return View(data);
    }

    public IActionResult AddEmployee()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> SaveEmployee(AddEmployeeDto model)
    {
        if (ModelState.IsValid)
        {
            bool isSucess = await _employeeService.AddEmployee(model);
            if (isSucess)
            {
                return RedirectToAction(nameof(Index));
            }

        }
        return View(nameof(AddEmployee), model);
    }
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
