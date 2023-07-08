using Dapper;
using EmpProjectManagement.Models;
using EmpProjectManagement.Api.Util;
using System.Data;

namespace EmpProjectManagement.Api.Repo.Project;
using Dapper;
using Microsoft.Data.SqlClient;

public class Project : IProject
{
    private readonly Context.IDbConnection _dbConnection;
    public Project(Context.IDbConnection dbConnection)
    {
        _dbConnection = dbConnection;
    }
   
    public async Task<List<ProjectDto>> GetAllAsync()
    {
        try
        {
            List<ProjectDto> serviceResponse = new List<ProjectDto>();

            using var connection = new SqlConnection(_dbConnection.DbConnectionString());

            var query = $"SELECT id,Name,Startdate [StartDate],Enddate [EndDate],Employees,SUM(DevCost + DBACost + TesterCost + BACost + Cost) as TotalCost " +
                $" from Project p (nolock) \r\n  OUTER APPLY (SELECT (2500 * Count(*)) DevCost FROM Employee e \r\n " +
                $" LEFT JOIN ProjectEmployee pe (nolock) on e.id = pe.EmployeeID\r\n " +
                $" LEFT JOIN JobTitle j (nolock) on j.id = e.JobtitleId" +
                $"\r\nWHERE j.id = 1 and pe.ProjectID = p.id ) as  Dev " +
                $"\r\n  OUTER APPLY (SELECT (3000 * Count(*)) DBACost FROM Employee e " +
                $"\r\n  LEFT JOIN ProjectEmployee pe (nolock) on e.id = pe.EmployeeID" +
                $"\r\n  LEFT JOIN JobTitle j (nolock) on j.id = e.JobtitleId" +
                $"\r\nWHERE j.id = 2 and pe.ProjectID = p.id ) as  DBA" +
                $" \r\n  OUTER APPLY (SELECT (1000 * Count(*)) TesterCost FROM Employee e " +
                $"\r\n  LEFT JOIN ProjectEmployee pe (nolock) on e.id = pe.EmployeeID" +
                $"\r\n  LEFT JOIN JobTitle j (nolock) on j.id = e.JobtitleId" +
                $"\r\nWHERE j.id = 3 and pe.ProjectID = p.id ) as  Tester " +
                $"\r\n  OUTER APPLY (SELECT (4500 * Count(*)) BACost FROM Employee e " +
                $"\r\n  LEFT JOIN ProjectEmployee pe (nolock) on e.id = pe.EmployeeID" +
                $"\r\n  LEFT JOIN JobTitle j (nolock) on j.id = e.JobtitleId" +
                $"\r\nWHERE j.id = 4 and pe.ProjectID = p.id ) as  BA" +
                $"\r\nCROSS APPLY (SELECT STUFF((SELECT ',' + Concat(e.Name,' ', e.Surname) \r\n     " +
                $"  FROM Employee e \r\n\t\t\t\t\t\t\t LEFT JOIN ProjectEmployee pe (nolock) on e.id = pe.EmployeeID\r\n    " +
                $"  WHERE p.id = pe.ProjectID FOR XML PATH('')),1,1,'') Employees) B\r\n\t\t\t\t\t\t  " +
                $" GROUP BY id,Name,Startdate,Enddate,Cost,DevCost,DBACost,TesterCost,BACost,Employees";
            
            var project = await connection.QueryAsync<ProjectDto>(query);
           
            serviceResponse = project.ToList();           
            return serviceResponse;
        }
        catch (Exception ex)
        {

            throw ex;
        }
    }

}
