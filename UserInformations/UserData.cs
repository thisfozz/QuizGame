using System;
using System.Collections.Generic;

namespace UserDataNamespace
{
    public class UserData
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public DateTime DateOfBirth { get; set; }
        public Dictionary<string, int> QuizResults { get; set; }

        public UserData(string Login, string Password, DateTime DateOfBirth)
        {
            this.Login = Login;
            this.Password = Password;
            this.DateOfBirth = DateOfBirth.Date;
            QuizResults = new Dictionary<string, int>();
        }
    }
}