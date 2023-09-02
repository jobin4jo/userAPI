using Employee.Data.Model;

namespace Employee.Data.IRepository
{
    public interface IEmployeeInformationRepository
    {
        Task<List<UserInformation>> GetEmployeeList();
        Task<bool>CreateEmployee(UserInformation userInformation);
        Task<bool>UpdateEmployee(UserInformation userInformation);
        Task<bool> DeleteEmployee(int Id);
        Task<UserInformation> GetEmployeeById(int Id);  
    }
}
