using EmpProjectManagement.Models;
using EmpProjectManagement.Web.Services.Contracts;

namespace EmpProjectManagement.Web.Services;

public class EmployeeService : IEmployeeService
{
    private readonly HttpClient httpClient;

    public EmployeeService(HttpClient httpClient)
    {
        this.httpClient = httpClient;
    }
    public async Task<bool> AddEmployee(AddEmployeeDto add)
    {
        try
        {
            var response = await httpClient.PostAsJsonAsync<AddEmployeeDto>("api/Employee", add);

            if (response.IsSuccessStatusCode)
            {
                if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                {
                    return false;
                }

                return true;

            }
            else
            {
                var message = await response.Content.ReadAsStringAsync();
                throw new Exception($"Http status:{response.StatusCode} Message -{message}");
            }

        }
        catch (Exception)
        {

            throw;
        }
    }

    public async Task<List<EmployeeDto>> GetEmployees()
    {
        try
        {
            var response = await this.httpClient.GetAsync("api/Employee");

            if (response.IsSuccessStatusCode)
            {

                return await response.Content.ReadFromJsonAsync<List<EmployeeDto>>();
            }
            else
            {
                var message = await response.Content.ReadAsStringAsync();
                throw new Exception($"Http status code: {response.StatusCode} message: {message}");
            }

        }
        catch (Exception)
        {
            //Log exception
            throw;
        }
    }
}
