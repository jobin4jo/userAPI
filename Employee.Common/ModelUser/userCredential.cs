using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee.Common.ModelUser
{
    public  class userCredential
    {
        public class UserModel
        {
            public string Username { get; set; }
            public string mobile { get; set; }
            public string Role { get; set; }

        }
        public class UserLogin
        {
            public string Username { get; set; }
            public string mobile { get; set; }
        }
        public class UserConstants
        {
            public static List<UserModel> Users = new List<UserModel>()
        {
            new UserModel() { Username = "jobin", mobile = "8525963520", Role = "Administrator"}
           
        };
        }
    }
}
