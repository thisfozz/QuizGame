using QuizSerializerNamespace;
using System;
using System.Collections.Generic;

namespace QuizNamespace
{
    public class QuizCreator
    {
        private QuizSerializer quizSerializer;

        public QuizCreator()
        {
            quizSerializer = new QuizSerializer();
        }

        public void CreateQuiz()
        {
            Dictionary<string, Dictionary<string, bool>> quiz = new Dictionary<string, Dictionary<string, bool>>();

            while (true)
            {
                Console.WriteLine("Введите вопрос (или введите 'save' для завершения и сохранения викторины):");
                string question = Console.ReadLine();

                if (question == "save") break;

                Dictionary<string, bool> answers = new Dictionary<string, bool>();

                Console.WriteLine("Введите варианты ответов (или введите 'save' для завершения вопроса):");

                while (true)
                {
                    Console.Write("Введите ответ: ");
                    string answer = Console.ReadLine();

                    if (answer == "save") break;

                    Console.WriteLine("Это правильный ответ? введите да или нет");
                    string isCorrectInput = Console.ReadLine();
                    bool isCorrectAnswer = isCorrectInput.ToLower() == "да";

                    answers.Add(answer, isCorrectAnswer);
                }

                quiz.Add(question, answers);
                Console.Clear();
            }

            Console.WriteLine("Введите название файла для сохранения викторины: ");
            string fileName = Console.ReadLine();
            string filePath = $"{fileName}.json";

            quizSerializer.SerializeQuiz(quiz, filePath);

            Console.WriteLine($"Викторина сохранена в файл: {filePath}");
        }
    }
}