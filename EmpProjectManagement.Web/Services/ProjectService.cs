using EmpProjectManagement.Models;
using EmpProjectManagement.Web.Services.Contracts;

namespace EmpProjectManagement.Web.Services;

public class ProjectService : IProjectService
{
    private readonly HttpClient httpClient;

    public ProjectService(HttpClient httpClient)
    {
        this.httpClient = httpClient;
    }

    public async Task<List<ProjectDto>> GetProjects()
    {
        try
        {
            var response = await this.httpClient.GetAsync("api/Project");

            if (response.IsSuccessStatusCode)
            {

                return await response.Content.ReadFromJsonAsync<List<ProjectDto>>();
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
