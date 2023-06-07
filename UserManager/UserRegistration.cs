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
            return users.Exists(l => l.Login == login);
        }

        public void Register(string login, string password, DateTime dateOfBirth)
        {
            if (!IsUserRegistered(login))
            {
                UserData newUser = new UserData(login, password, dateOfBirth);
                users.Add(newUser);
                LoadData.SaveUserData(users);
                Console.WriteLine("Пользователь успешно зарегистрирован");
            }
            else
            {
                Console.WriteLine("Пользователь с таким логином уже зарегистрирован");
            }
        }
    }
}
