using Newtonsoft.Json;
using System;

namespace UserDataNamespace
{
    public class UserData
    {
        [JsonProperty]
        public string Login { get; set; }
        public string Password { get; set; }
        public DateTime DateOfBirth { get; set; }

        public UserData(string Login, string Password, DateTime DateOfBirth)
        {
            this.Login = Login;
            this.Password = Password;
            this.DateOfBirth = DateOfBirth;
        }
    }
}