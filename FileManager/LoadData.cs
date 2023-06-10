using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using UserDataNamespace;
using Formatting = Newtonsoft.Json.Formatting;

namespace FileManagerNamespace
{
    public class LoadData
    {
        private const string FilePath = "UserData.json";

        public static List<UserData> LoadUserData()
        {
            if (File.Exists(FilePath))
            {
                string jsonData = File.ReadAllText(FilePath);
                var deserializedData = JsonConvert.DeserializeObject<List<UserData>>(jsonData);
                if (deserializedData != null)
                {
                    return deserializedData;
                }
            }

            return new List<UserData>();
        }
        public static void SaveUserData(List<UserData> users)
        {
            string jsonData = JsonConvert.SerializeObject(users, Formatting.Indented);
            File.WriteAllText(FilePath, jsonData);
        }
    }
}