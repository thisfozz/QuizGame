using QuestionQuizNamespace;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace QuizSerializerNamespace
{
    public class QuizSerializer
    {
        public void SerializeQuiz(List<QuestionQuiz> quiz, string nameQuiz)
        {
            string json = JsonConvert.SerializeObject(quiz, Formatting.Indented);
            File.WriteAllText(nameQuiz, json);
        }
        public List<QuestionQuiz> DeserializeQuiz(string nameQuiz)
        {
            if (File.Exists(nameQuiz))
            {
                string jsonData = File.ReadAllText(nameQuiz);
                var deserializedData = JsonConvert.DeserializeObject<List<QuestionQuiz>>(jsonData);
                return deserializedData;
            }

            return new List<QuestionQuiz>();
        }
    }
}