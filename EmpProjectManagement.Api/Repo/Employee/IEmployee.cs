using EmpProjectManagement.Models;
using EmpProjectManagement.Api.Util;

namespace EmpProjectManagement.Api.Repo.Employee;

public interface IEmployee
{
    Task<List<EmployeeDto>> GetAllAsync();
    Task<ServiceResponse<string>> AddAsync(AddEmployeeDto add);
}
