using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserDataNamespace;

namespace QuizSerializerNamespace
{
    public class QuizSerializer
    {
        // private string HistoryQuizFilePath = "HistoryQuiz.json";
        // private string GeographyQuizFilePath = "GeographyQuiz.json";
        // private string CSharpQuizFilePath = "CSharpQuiz.json";

        public void SerializeQuiz(Dictionary<string, Dictionary<string, bool>> quiz, string nameQuiz)
        {
            string json = JsonConvert.SerializeObject(quiz, Formatting.Indented);
            System.IO.File.WriteAllText(nameQuiz, json);
        }

        public Dictionary<string, Dictionary<string, bool>> DeserializeQuiz(string nameQuiz)
        {
            if (File.Exists(nameQuiz))
            {
                string jsonData = File.ReadAllText(nameQuiz);
                var deserializedData = JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, bool>>>(jsonData);
                return deserializedData;
            }
            else
            {
                return new Dictionary<string, Dictionary<string, bool>>();
            }
        }
    }
}
