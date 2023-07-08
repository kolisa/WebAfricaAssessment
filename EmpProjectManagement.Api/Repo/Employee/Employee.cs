using Dapper;
using EmpProjectManagement.Models;
using EmpProjectManagement.Api.Util;
using Microsoft.Data.SqlClient;

namespace EmpProjectManagement.Api.Repo.Employee;

public class Employee : IEmployee
{
    private readonly Context.IDbConnection _connectionString;
    public Employee(Context.IDbConnection connectionString)
    {
        _connectionString = connectionString;
    }
    public async Task<ServiceResponse<string>> AddAsync(AddEmployeeDto add)
    {
        try
        {
            ServiceResponse<string> serviceResponse = new ServiceResponse<string>();
            var query = $"INSERT INTO Employee (Name,Surname,JobTitleId,DateOfBirth) " +
                $"Values (@Name,@Surname,@JobTitleId,@DateOfBirth)";
            using var connection = new SqlConnection(_connectionString.DbConnectionString());
            

            var row = await connection.ExecuteAsync(query,add);
            if (row == 0)
            {

                serviceResponse.Message = "Error While saving employee!!";
                serviceResponse.Success = false;
                return serviceResponse;
            }
            serviceResponse.Message = "Employee has been Added succesful";
            serviceResponse.Success = true;
            return serviceResponse;
        }
        catch (Exception)
        {

            throw;
        }
    }

    public async Task<List<EmployeeDto>> GetAllAsync()
    {
        try
        {
            List<EmployeeDto> serviceResponse = new List<EmployeeDto>();

            using var connection = new SqlConnection(_connectionString.DbConnectionString());

            var query = $"SELECT e.id [ID],e.Name,e.Surname,j.[JobTitle], e.DateOfBirth FROM [dbo].[Employee] e (nolock) \r\n  INNER JOIN JobTitle j (nolock) on j.id = e.JobTitleId";

            var employee = await connection.QueryAsync<EmployeeDto>(query);

            serviceResponse = employee.ToList();
            return serviceResponse;
        }
        catch (Exception ex)
        {

            throw ex;
        }
    }
}
