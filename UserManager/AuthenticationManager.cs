using FileManagerNamespace;
using UserDataNamespace;
using System;
using System.Collections.Generic;
using AesEncryptionNamespace;

namespace AuthenticationManagerNamespace
{
    public class AuthenticationManager
    {
        private static List<UserData> registeredUsers; // ?? static ??
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
            SaveRegisteredUsers();
            currentUser = newUser;
            return true;
        }

        private void SaveRegisteredUsers()
        {
            string jsonUserData = LoadData.toJsonUserData(registeredUsers);
            LoadData.SerializeUserData(jsonUserData);
        }

        public List<UserData> GetAllRegisteredUser()
        {
            return registeredUsers;
        }
        public void UpdatePassword(string login,string password)
        {
            if(currentUser != null && currentUser.Login == login)
            {
                currentUser.Password = password;
            }
        }
        public bool LoginUser(string login, string password)
        {
            //registeredUsers = LoadData.LoadUserData(); // костыль
            AesEncryption aesEncryption = new AesEncryption();
            UserData user = registeredUsers.Find(u => u.Login == login);

            if (user == null) return false;
            string decryptedPassword = aesEncryption.Decrypt(user.Password);
            if (password == decryptedPassword)
            {
                currentUser = user;
                return true;
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