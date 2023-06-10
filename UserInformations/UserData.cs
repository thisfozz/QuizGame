using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserLoginNamespace;

namespace UserDataNamespace
{
    public class UserData
    {
        [JsonProperty]
        public static string Login { get; set; }
        public string Password { get; set; }
        public DateTime DateOfBirth { get; set; }

        public UserData(string LoginUser, string Password, DateTime DateOfBirth)
        {
            Login = LoginUser;
            this.Password = Password;
            this.DateOfBirth = DateOfBirth;
        }

        public string getLogin()
        {
            return Login;
        }
    }
}