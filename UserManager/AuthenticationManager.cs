using FileManagerNamespace;
using UserDataNamespace;
using System;
using System.Collections.Generic;
using AesEncryptionNamespace;

namespace AuthenticationManagerNamespace
{
    public class AuthenticationManager
    {
        private List<UserData> registeredUsers;
        private UserData currentUser;

        public AuthenticationManager()
        {
            registeredUsers = LoadData.LoadUserData();
            currentUser = null;
        }
        public bool RegisterUser(string loginUser, string passwordUser, DateTime birthdate)
        {

            if (registeredUsers.Exists(u => u.Login == loginUser)) return false;

            UserData newUser = new UserData(loginUser, passwordUser, birthdate);
            registeredUsers.Add(newUser);
            LoadData.SaveUserData(registeredUsers);
            currentUser = newUser;

            return true;

        }
        public bool LoginUser(string login, string password)
        {
            AesEncryption aesEncryption = new AesEncryption();
            UserData user = registeredUsers.Find(u => u.Login == login);

            if (user != null)
            {
                string decryptedPassword = aesEncryption.Decrypt(user.Password);
                if (password == decryptedPassword)
                {
                    currentUser = user;
                    return true;
                }
            }

            return false;
        }
        public void LogoutUser()
        {
            currentUser = null;
        }
        public string GetLoggedInUserLogin()
        {
            return currentUser?.Login;
        }
        public UserData GetCurrectUser()
        {
            return currentUser;
        }
    }
}