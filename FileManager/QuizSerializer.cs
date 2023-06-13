using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace QuizSerializerNamespace
{
    public class QuizSerializer
    {
        public void SerializeQuiz(Dictionary<string, Dictionary<string, bool>> quiz, string nameQuiz)
        {
            string json = JsonConvert.SerializeObject(quiz, Formatting.Indented);
            File.WriteAllText(nameQuiz, json);
        }

        public Dictionary<string, Dictionary<string, bool>> DeserializeQuiz(string nameQuiz)
        {
            if (File.Exists(nameQuiz))
            {
                string jsonData = File.ReadAllText(nameQuiz);
                var deserializedData = JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, bool>>>(jsonData);
                return deserializedData;
            }

            return new Dictionary<string, Dictionary<string, bool>>();
        }
    }
}