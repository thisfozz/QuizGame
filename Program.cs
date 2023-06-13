using QuizCreatorNamespace;
using UserInterfaseMenuNamespace;
using Alba.CsConsoleFormat;
using static System.ConsoleColor;
using System;
using System.Collections.Generic;

namespace TestCSExam
{
    internal class Program
    {
        static void Main(string[] args)
        {
            UserInterfaseMenu userInterfaseMenu = new UserInterfaseMenu();
            userInterfaseMenu.Start();

            //QuizCreator quizCreator = new QuizCreator();
            //quizCreator.CreateQuiz();

        }
    }
}
