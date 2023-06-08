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

        public bool LoginUser(string login, string password, out string message)
        {
            bool loggedIn = false;
            AesEncryption aesEncryption = new AesEncryption();
            message = string.Empty;

            UserData user = registeredUsers.Find(u => u.Login == login);
            if (user != null)
            {
                string decryptedPassword = aesEncryption.Decrypt(user.Password);
                if (password == decryptedPassword)
                {
                    loggedIn = true;
                    message = "Вход выполнен успешно.";
                }
                else
                {
                    message = "Неверный логин или пароль. Повторите попытку.";
                }
            }
            else
            {
                message = "Пользователь не найден.";
            }

            return loggedIn;
        }
    }
}