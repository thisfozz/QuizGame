using FileManagerNamespace;
using UserDataNamespace;
using System;
using System.Collections.Generic;
using AesEncryptionNamespace;

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
            AesEncryption aesEncryption = new AesEncryption();

            do
            {
                Console.WriteLine("Введите логин:");
                string login = Console.ReadLine();
                Console.WriteLine("Введите пароль:");
                string enteredPassword = Console.ReadLine();

                UserData user = registeredUsers.Find(u => u.Login == login);
                if (user != null)
                {
                    string decryptedPassword = aesEncryption.Decrypt(user.Password);
                    if (enteredPassword == decryptedPassword)
                    {
                        Console.WriteLine("Вход выполнен успешно.");
                        loggedIn = true;
                    }
                    else
                    {
                        Console.WriteLine("Неверный логин или пароль. Повторите попытку.");
                    }
                }
                else
                {
                    Console.WriteLine("Неверный логин или пароль. Повторите попытку.");
                }
            } while (!loggedIn);
        }
    }
}