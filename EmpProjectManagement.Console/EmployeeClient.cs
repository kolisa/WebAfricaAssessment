using EmpProjectManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace EmpProjectManagement.Console;
public class EmployeeClient
{
    private readonly HttpClient _httpClient;

    public EmployeeClient(HttpClient httpClient) => _httpClient = httpClient;

    public async Task<HttpResponseMessage> AddemployeeAsync(AddEmployeeDto add) => await _httpClient.PostAsJsonAsync<AddEmployeeDto>("api/employee", add);
}
