using QuestionQuizNamespace;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace QuizSerializerNamespace
{
    public class QuizSerializer
    {
        public string toSonQuiz(List<QuestionQuiz> quiz)
        {
            string json = JsonConvert.SerializeObject(quiz, Formatting.Indented);

            return json;
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

        public void SerializeQuiz(string jsonQuiz, string nameQuiz)
        {
            File.WriteAllText(nameQuiz, jsonQuiz);
        }
    }
}