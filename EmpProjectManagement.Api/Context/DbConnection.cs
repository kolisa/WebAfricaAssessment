using System.Data;

namespace EmpProjectManagement.Api.Context;

public class DbConnection : IDbConnection
{
    private readonly IConfiguration _configuration;
    public DbConnection(IConfiguration configuration)
    {
        _configuration = configuration;
    }


    public string DbConnectionString()
    {
        try
        {
            return _configuration.GetSection("ConnectionStrings:DefaultConnection").Value;
        }
        catch (Exception)
        {

            throw;
        }
    }


}
