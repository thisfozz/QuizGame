using FileManagerNamespace;
using UserDataNamespace;
using System;
using System.Collections.Generic;

namespace UserLoginNamespace
{
    public class Login
    {
        private List<UserData> registeredUsers;

        public Login()
        {
            registeredUsers = LoadData.LoadUserData();
        }

        public void LoginUser()
        {
            bool loggedIn = false;

            do
            {
                Console.WriteLine("Введите логин:");
                string login = Console.ReadLine();
                Console.WriteLine("Введите пароль:");
                string password = Console.ReadLine();

                UserData user = registeredUsers.Find(u => u.Login == login && u.Password == password);
                if (user != null)
                {
                    Console.WriteLine("Вход выполнен успешно.");
                    loggedIn = true;
                }
                else
                {
                    Console.WriteLine("Неверный логин или пароль. Повторите попытку.");
                }
            } while (!loggedIn);
        }
    }
}
