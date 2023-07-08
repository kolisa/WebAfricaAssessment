using EmpProjectManagement.Models;

namespace EmpProjectManagement.Web.Services.Contracts;

public interface IEmployeeService
{
    Task<List<EmployeeDto>> GetEmployees();
    Task<bool> AddEmployee(AddEmployeeDto add);
}
