using Alba.CsConsoleFormat;
using QuizCreatorNamespace;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UserInterfaseMenuNamespace;

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
