using ClosedXML.Excel;
using Employee.Data.IRepository;
using Employee.Data.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace Employee.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    [AllowAnonymous]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeInformationRepository _employeeinformation;
        public EmployeeController(IEmployeeInformationRepository employeeInformationRepository)
        {
              _employeeinformation = employeeInformationRepository; 
        }
        [HttpGet("List")]
        public async Task<ActionResult> GetAllEmployee()
        {
            try
            {
                var employeeList = await _employeeinformation.GetEmployeeList();
                return new CreatedResult(string.Empty, new { Code = 200, Status = true, Message = "", Data = employeeList });
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


        [HttpGet("ExportExcel")]
        public async Task<ActionResult> GetExcelfile()
        {

            var employeeData = await GetEmployeeData();
            string base64String = string.Empty;
            using (XLWorkbook wb = new XLWorkbook())
            {
                var sheet = wb.AddWorksheet(employeeData, "Employee Records");
                sheet.Columns(1, 5).Style.Font.FontColor = XLColor.Black;
                using (MemoryStream ms = new MemoryStream())
                {
                    wb.SaveAs(ms);
                    //return File(ms.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "EmployeeList.xlsx");
                     base64String = Convert.ToBase64String(ms.ToArray());
                }

            }
            return new CreatedResult(string.Empty, new { Code = 200, Status = true, Message = "", Data = base64String });



          
        }
        [NonAction]
        private async Task<DataTable> GetEmployeeData()
        {
            DataTable data = new DataTable();
            data.TableName = "Employee List";
            data.Columns.Add("Name",typeof(string));
            data.Columns.Add("PhoneNumber",typeof(string));
            data.Columns.Add("Email",typeof(string));
            data.Columns.Add("Department", typeof(string));
            data.Columns.Add("Designation", typeof(string));
            var empdata = await _employeeinformation.GetEmployeeList();
            if(empdata.Count > 0)
            {
                empdata.ForEach(x =>
                {
                    data.Rows.Add(x.Name, x.PhoneNumber, x.Email, x.Department, x.Designation);
                });
            }
            return data;

        }


        [HttpPost("Insert")]
        public async Task<ActionResult> CreateEmployee(UserInformation user)
        {
            try
            {
                var res= await _employeeinformation.CreateEmployee(user);
                return new CreatedResult(string.Empty, new { Code = 200, Status = true, Message = "", Data = res });
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpPut("Update")]
        public async Task<ActionResult>UpdateEmployee(UserInformation userInformation)
        {
           
            try
            {
                var res = await _employeeinformation.UpdateEmployee(userInformation);
                return new CreatedResult(string.Empty, new { Code = 200, Status = true, Message = "", Data = res });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpDelete("DeleteById")]
        public async Task<ActionResult>DeleteById(int Id)
        {
            try
            {
                var res= await _employeeinformation.DeleteEmployee(Id);
                return new CreatedResult(string.Empty, new { Code = 200, Status = true, Message = "", Data = res });
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpGet("GetById")]
        public async Task<ActionResult>GetEmployeeById(int Id)
        {
            try
            {
                var res= await _employeeinformation.GetEmployeeById(Id);
                return new CreatedResult(string.Empty, new { Code = 200, Status = true, Message = "", Data = res });
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
