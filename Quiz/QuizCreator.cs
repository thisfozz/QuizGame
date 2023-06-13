using QuestionQuizNamespace;
using Newtonsoft.Json;
using QuestionQuizNamespace;
using QuizSerializerNamespace;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AnswerQuizNamespace;

namespace QuizCreatorNamespace
{
    public class QuizCreator
    {
        private static readonly QuizSerializer quizSerializer = new QuizSerializer();
        public void CreateQuiz()
        {
            List<QuestionQuiz> quiz = new List<QuestionQuiz>();

            while (true)
            {
                Console.WriteLine("Введите вопрос (или введите 'save' для завершения и сохранения викторины):");
                string question = Console.ReadLine();

                if (question == "save")
                    break;

                List<AnswerQuiz> answers = new List<AnswerQuiz>();

                Console.WriteLine("Введите варианты ответов (или введите 'save' для завершения вопроса):");

                while (true)
                {
                    Console.Write("Введите ответ: ");
                    string answerText = Console.ReadLine();

                    if (answerText == "save")
                        break;

                    Console.WriteLine("Это правильный ответ? Введите 'да' или 'нет':");
                    string isCorrectInput = Console.ReadLine();
                    bool isCorrectAnswer = isCorrectInput.ToLower() == "да";

                    AnswerQuiz answer = new AnswerQuiz
                    {
                        Answer = answerText,
                        IsCorrectAnswer = isCorrectAnswer
                    };

                    answers.Add(answer);
                }

                QuestionQuiz questionQuiz = new QuestionQuiz
                {
                    Question = question,
                    Answers = answers
                };

                quiz.Add(questionQuiz);
                Console.Clear();
            }

            Console.WriteLine("Введите название файла для сохранения викторины: ");
            string fileName = Console.ReadLine();
            string filePath = $"{fileName}.json";

            quizSerializer.SerializeQuiz(quiz, filePath);

            Console.WriteLine($"Викторина сохранена в файле: {filePath}");
        }

    }
}
