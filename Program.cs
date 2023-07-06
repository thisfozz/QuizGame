using QuizCreatorNamespace;
using System.Threading;
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
