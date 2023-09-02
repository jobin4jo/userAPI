using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Employee.DataContracts
{
    public class DapperContext
    {
        public readonly IConfiguration _configuration;
        public readonly string _connectionString;
        public DapperContext(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("Application_Database");
        }
        public IDbConnection CreateConnection()
        => new SqlConnection(_connectionString);
    }
}
