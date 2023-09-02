using Dapper;
using Employee.Data.IRepository;
using Employee.Data.Model;
using Employee.DataContracts;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee.Data.Repository
{
    public class EmployeeInformationRepository : IEmployeeInformationRepository
    {
        private readonly DapperContext _context;
        public EmployeeInformationRepository(DapperContext context)
        {
            _context= context;  
        }

        public async Task<bool> CreateEmployee(UserInformation userInformation)
        {
         var query = "INSERT INTO UserInformation (Name, PhoneNumber, Email, Department, Designation,status) VALUES(@Name,@PhoneNumber, @Email,@Department,@Designation, 1)";
            var parameters = new DynamicParameters();
            parameters.Add("Name", userInformation.Name, DbType.String);
            parameters.Add("PhoneNumber",userInformation.PhoneNumber, DbType.String);
            parameters.Add("Email", userInformation.Email, DbType.String);
            parameters.Add("Department", userInformation.Department, DbType.String);
            parameters.Add("Designation", userInformation.Designation, DbType.String);
            parameters.Add("status", userInformation.Status, DbType.Int32);
            using (var connection = _context.CreateConnection())
            {
                try
                {
                    await connection.ExecuteAsync(query, parameters);
                    return true;
                }catch(Exception ex)
                {
                    return false;
                }
               
            }

           
        }

        public async  Task<bool> DeleteEmployee(int Id)
        {
            var query = "DELETE FROM UserInformation WHERE EmpId=@EmpId";
            using (var connection = _context.CreateConnection())
            {
                try
                {
                    await connection.ExecuteAsync(query,new {EmpId=Id });
                    return true;
                }
                catch(Exception ex)
                {
                    return false;
                }
            }
          
        }

        public async Task<UserInformation> GetEmployeeById(int Id)
        {
            var query = "SELECT * FROM UserInformation WHERE EmpId = @EmpId";
            using (var connection = _context.CreateConnection())
            {
                var userinfo = await connection.QuerySingleOrDefaultAsync<UserInformation>(query, new { EmpId = Id });
                return userinfo;
            }
        }

        public async Task<List<UserInformation>> GetEmployeeList()
        {
            var query = "select * from UserInformation";
            using (var connection = _context.CreateConnection())
            {
                var list = await connection.QueryAsync<UserInformation>(query);
                return list.ToList();
            }
        }

        public async  Task<bool> UpdateEmployee(UserInformation userInformation)
        {
            var query = "UPDATE UserInformation SET Name =@Name,PhoneNumber =@PhoneNumber,Email=@Email,Department=@Department,Designation=@Designation,status=@status WHERE EmpId=@EmpId";
            var parameters = new DynamicParameters();
            parameters.Add("EmpId", userInformation.EmpId, DbType.Int32);
            parameters.Add("Name", userInformation.Name, DbType.String);
            parameters.Add("PhoneNumber", userInformation.PhoneNumber, DbType.String);
            parameters.Add("Email", userInformation.Email, DbType.String);
            parameters.Add("Department", userInformation.Department, DbType.String);
            parameters.Add("Designation", userInformation.Designation, DbType.String);
            parameters.Add("status", userInformation.Status, DbType.Int32);
            using (var connection = _context.CreateConnection())
            {
                try
                {
                    await connection.ExecuteAsync(query, parameters);
                    return true;
                }
                catch (Exception ex)
                {
                    return false;
                }

            }
        }
    }
}
