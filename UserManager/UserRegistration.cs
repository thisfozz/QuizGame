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

        public void Register()
        {
            Console.Write("Введите логин: ");
            string loginUser = Console.ReadLine();

            Console.Write("Введите пароль: ");
            string passwordUser = Console.ReadLine();

            Console.WriteLine("Введите дату рождения (дд-мм-гггг): ");
            DateTime birthdate = DateTime.Parse(Console.ReadLine());

            if (!IsUserRegistered(loginUser))
            {
                UserData newUser = new UserData(loginUser, passwordUser, birthdate);
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