using System;
using System.Collections.Generic;
using UserDataNamespace;
using FileManagerNamespace;

namespace UserRegistrationNamespace
{
    public class UserRegistration
    {
        private List<UserData> users;

        public UserRegistration()
        {
            users = LoadData.LoadUserData();
        }

        private bool IsUserRegistered(string login)
        {
            return users.Exists(l => l.getLogin() == login);
        }

        public bool Register(string loginUser, string passwordUser, DateTime birthdate)
        {
            if (!IsUserRegistered(loginUser))
            {
                UserData newUser = new UserData(loginUser, passwordUser, birthdate);
                users.Add(newUser);
                LoadData.SaveUserData(users);
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}