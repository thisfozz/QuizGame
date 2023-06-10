using AesEncryptionNamespace;
using QuizNamespace;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
