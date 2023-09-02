using Employee.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static Employee.Common.ModelUser.userCredential;

namespace Employee.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IConfiguration _config;
        public UserController(IConfiguration config)
        {
            _config = config;
        }

        [HttpPost("Login")]
        ///login testing name="jobin" mobile="8525963520"
        public ActionResult Login([FromBody] UserLogin userLogin)
        {
            var user =Authenticate(userLogin);

            if (user != null)
            {
                string role = "Administrator";
                var token = jwt.GenerateToken(userLogin.Username, userLogin.mobile, _config["Jwt:Key"], _config["Jwt:Issuer"], _config["Jwt:Audience"],role);
                return new CreatedResult(string.Empty, new { Code = 200, Status = true, Message = "", Data = token });
            }

            return NotFound("User not found");
        }
        [NonAction]
        private UserModel Authenticate(UserLogin userLogin)
        {
            var currentUser = UserConstants.Users.FirstOrDefault(o => o.Username.ToLower() == userLogin.Username.ToLower() && o.mobile == userLogin.mobile);

            if (currentUser != null)
            {
                return currentUser;
            }

            return null;
        }
    }
}
