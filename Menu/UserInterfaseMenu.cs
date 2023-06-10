using System;
using AesEncryptionNamespace;
using UserLoginNamespace;
using UserRegistrationNamespace;
using System.Threading;
using System.Net;
using DataCorrectnessNamespace;
using QuizSerializerNamespace;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using Newtonsoft.Json.Linq;
using System.IO;
using UserDataNamespace;
using Newtonsoft.Json;

namespace UserInterfaseMenuNamespace
{
    public class UserInterfaseMenu
    {
        private UserRegistration registration = new UserRegistration();
        private Login login = new Login();
        private AesEncryption aesEncryption = new AesEncryption();

        private void AuthorizationForm()
        {
            Console.WriteLine("\t\t\t\t╔═══════════════════════════════════════════════════════╗");
            Console.WriteLine("\t\t\t\t║                                                       ║");
            Console.WriteLine("\t\t\t\t║                  Добро пожаловать                     ║");
            Console.WriteLine("\t\t\t\t║                                                       ║");
            Console.WriteLine("\t\t\t\t╠═══════════════════════════════════════════════════════║");
            Console.WriteLine("\t\t\t\t║                                                       ║");
            Console.WriteLine("\t\t\t\t║   1. Войти в аккаунт                                  ║");
            Console.WriteLine("\t\t\t\t║                                                       ║");
            Console.WriteLine("\t\t\t\t║   2. Создать аккаунт                                  ║");
            Console.WriteLine("\t\t\t\t║                                                       ║");
            Console.WriteLine("\t\t\t\t╚═══════════════════════════════════════════════════════╝");

            ConsoleKeyInfo keyInfo;

            bool validArgument = false;

            while (!validArgument)
            {
                keyInfo = Console.ReadKey();
                if (keyInfo.Key >= ConsoleKey.D1 && keyInfo.Key <= ConsoleKey.D2)
                {
                    if (keyInfo.Key == ConsoleKey.D1)
                    {
                        LoginForm();
                    }
                    else if (keyInfo.Key == ConsoleKey.D2)
                    {
                        RegistrationForm();
                    }
                }
                else
                {
                    Console.WriteLine("\nНекорректный выбор. Повторите ввод.");

                }
            }
        }
        private void LoginForm()
        {
            Console.Clear();
            bool isCorrectData = false, isAuthorization = false;
            int LoginAttempt = 0;
            string text = string.Empty;

            do
            {
                Console.WriteLine("\t\t\t\t╔═══════════════════════════════════════════════════════╗");
                Console.WriteLine("\t\t\t\t║                                                       ║");
                Console.WriteLine("\t\t\t\t║                     Авторизация                       ║");
                Console.WriteLine("\t\t\t\t║                                                       ║");
                Console.WriteLine("\t\t\t\t╠═══════════════════════════════════════════════════════║");
                Console.WriteLine("\t\t\t\t║                                                       ║");
                Console.WriteLine("\t\t\t\t║    Введите логин:                                     ║");
                Console.WriteLine("\t\t\t\t║                                                       ║");
                Console.WriteLine("\t\t\t\t║    Введите пароль:                                    ║");
                Console.WriteLine("\t\t\t\t║                                                       ║");
                Console.WriteLine("\t\t\t\t╚═══════════════════════════════════════════════════════╝");

                Console.SetCursorPosition(52, 6);
                string loginAuthUser = Console.ReadLine();

                while (!DataCorrectness.isCheckLogin(loginAuthUser))
                {
                    Console.SetCursorPosition(90, 6);
                    Console.Write("Недопустимый формат логина");
                    Thread.Sleep(1000);

                    Console.SetCursorPosition(90, 6);
                    Console.Write(new string(' ', "Недопустимый формат логина".Length));
                    Console.SetCursorPosition(52, 6);
                    Console.Write(new string(' ', loginAuthUser.Length));

                    Console.SetCursorPosition(52, 6);
                    loginAuthUser = Console.ReadLine();
                }

                Console.SetCursorPosition(53, 8);
                string passwordAuthUser = Console.ReadLine();

                while (!DataCorrectness.isCheckPassword(passwordAuthUser))
                {
                    Console.SetCursorPosition(90, 8);
                    Console.Write("Недопустимый формат пароля");
                    Thread.Sleep(1000);

                    Console.SetCursorPosition(90, 8);
                    Console.Write(new string(' ', "Недопустимый формат пароля".Length));
                    Console.SetCursorPosition(53, 8);
                    Console.Write(new string(' ', passwordAuthUser.Length));

                    Console.SetCursorPosition(53, 8);
                    passwordAuthUser = Console.ReadLine();
                }

                string message = string.Empty;

                isAuthorization = login.LoginUser(loginAuthUser, passwordAuthUser, out message);

                if (isAuthorization)
                {
                    isCorrectData = true;
                    Console.Clear();
                    Console.WriteLine("\t\t\t\t╔════════════════════════════════════════════════════════════╗");
                    Console.WriteLine("\t\t\t\t║                                                            ║");
                    Console.WriteLine($"\t\t\t\t║                {message}                      ║");
                    Console.WriteLine("\t\t\t\t║                                                            ║");
                    Console.WriteLine("\t\t\t\t╚════════════════════════════════════════════════════════════╝");

                    Thread.Sleep(2000);

                    Console.Clear();

                    MainMenu();
                }
                else
                {
                    LoginAttempt++;
                    Console.Clear();
                    Console.WriteLine("\t\t\t\t╔═══════════════════════════════════════════════════════════╗");
                    Console.WriteLine("\t\t\t\t║                                                           ║");
                    Console.WriteLine($"\t\t\t\t║              {message}                  ║");
                    Console.WriteLine("\t\t\t\t║                                                           ║");
                    Console.WriteLine("\t\t\t\t╚═══════════════════════════════════════════════════════════╝");
                    Thread.Sleep(1000);

                    Console.Clear();

                    if (LoginAttempt > 3)
                    {
                        Console.WriteLine("\t\t\t\t╔═══════════════════════════════════════════════════════╗");
                        Console.WriteLine("\t\t\t\t║                                                       ║");
                        Console.WriteLine("\t\t\t\t║           Перейти обратно к форме авторизации?        ║");
                        Console.WriteLine("\t\t\t\t║                                                       ║");
                        Console.WriteLine("\t\t\t\t║           Введите yes or no:                          ║");
                        Console.WriteLine("\t\t\t\t║                                                       ║");
                        Console.WriteLine("\t\t\t\t╚═══════════════════════════════════════════════════════╝");
                        Console.SetCursorPosition(63, 4);
                        text = Console.ReadLine();
                        if (text == "yes" || text.ToUpper() == "YES")
                        {
                            isCorrectData = true;
                            Console.Clear();
                            AuthorizationForm();
                        }
                        else if(text == "no" ||  text.ToUpper() == "NO")
                        {
                            Console.Clear();
                            isCorrectData = false;
                        }
                    }
                    else
                    {
                        isCorrectData = false;
                    }
                }
            } while (!isCorrectData);
        }
        private void RegistrationForm()
        {
            Console.Clear();
            Console.WriteLine("\t\t\t\t╔═══════════════════════════════════════════════════════╗");
            Console.WriteLine("\t\t\t\t║                                                       ║");
            Console.WriteLine("\t\t\t\t║                     Регистрация                       ║");
            Console.WriteLine("\t\t\t\t║                                                       ║");
            Console.WriteLine("\t\t\t\t╠═══════════════════════════════════════════════════════║");
            Console.WriteLine("\t\t\t\t║                                                       ║");
            Console.WriteLine("\t\t\t\t║    Логин:                                             ║");
            Console.WriteLine("\t\t\t\t║                                                       ║");
            Console.WriteLine("\t\t\t\t║    Пароль:                                            ║");
            Console.WriteLine("\t\t\t\t║                                                       ║");
            Console.WriteLine("\t\t\t\t║    Дата рождения:                                     ║");
            Console.WriteLine("\t\t\t\t║                                                       ║");
            Console.WriteLine("\t\t\t\t╚═══════════════════════════════════════════════════════╝");

            bool isRegistration = false;
            string text = string.Empty;

            Console.SetCursorPosition(44, 6);
            string loginRegistrationUser = Console.ReadLine();

            while (!DataCorrectness.isCheckLogin(loginRegistrationUser))
            {
                Console.SetCursorPosition(90, 6);
                Console.Write("Недопустимый формат логина");
                System.Threading.Thread.Sleep(1000);

                Console.SetCursorPosition(90, 6);
                Console.Write(new string(' ', "Недопустимый формат логина".Length));
                Console.SetCursorPosition(44, 6);
                Console.Write(new string(' ', loginRegistrationUser.Length));

                Console.SetCursorPosition(44, 6);
                loginRegistrationUser = Console.ReadLine();
            }

            Console.SetCursorPosition(45, 8);
            string passwordRegistrationUser = Console.ReadLine();

            while (!DataCorrectness.isCheckPassword(passwordRegistrationUser))
            {
                Console.SetCursorPosition(90, 8);
                Console.Write("Недопустимый формат пароля");
                System.Threading.Thread.Sleep(1000);

                Console.SetCursorPosition(90, 8);
                Console.Write(new string(' ', "Недопустимый формат пароля".Length));
                Console.SetCursorPosition(45, 8);
                Console.Write(new string(' ', passwordRegistrationUser.Length));

                Console.SetCursorPosition(45, 8);
                passwordRegistrationUser = Console.ReadLine();
            }

            Console.SetCursorPosition(52, 10);
            string dateBirthRegistrationUser = Console.ReadLine();

            while (!DataCorrectness.IsCheckDate(dateBirthRegistrationUser))
            {
                Console.SetCursorPosition(90, 10);
                Console.Write("Недопустимый формат даты");
                System.Threading.Thread.Sleep(1000); // Приостановка на 1 секунду

                // Очистить сообщение об ошибке и поле ввода
                Console.SetCursorPosition(90, 10);
                Console.Write(new string(' ', "Недопустимый формат даты".Length));
                Console.SetCursorPosition(52, 10);
                Console.Write(new string(' ', passwordRegistrationUser.Length));

                Console.SetCursorPosition(52, 10);
                passwordRegistrationUser = Console.ReadLine();
            }

            string passwordEncrypt = aesEncryption.Encrypt(passwordRegistrationUser);
            DateTime correctData = DataCorrectness.ConvertToDate(dateBirthRegistrationUser);
            isRegistration = registration.Register(loginRegistrationUser, passwordEncrypt, correctData);

            if (isRegistration)
            {
                Console.Clear();
                Console.WriteLine("\t\t\t\t╔═══════════════════════════════════════════════════════╗");
                Console.WriteLine("\t\t\t\t║                                                       ║");
                Console.WriteLine("\t\t\t\t║             Регистрация прошла успешно                ║");
                Console.WriteLine("\t\t\t\t║                                                       ║");
                Console.WriteLine("\t\t\t\t╚═══════════════════════════════════════════════════════╝");
                Thread.Sleep(2000);
                MainMenu();
            }
            else
            {
                Console.Clear();
                Console.WriteLine("\t\t\t\t╔═══════════════════════════════════════════════════════╗");
                Console.WriteLine("\t\t\t\t║                                                       ║");
                Console.WriteLine("\t\t\t\t║   Пользователь с таким логином уже зарегистрирован    ║");
                Console.WriteLine("\t\t\t\t║                                                       ║");
                Console.WriteLine("\t\t\t\t╚═══════════════════════════════════════════════════════╝");
            }
        }

        private void HistoryQuiz()
        {
            string HistoryQuizFilePath = "History.json";

            QuizSerializer quizSerializer = new QuizSerializer();

            Dictionary<string, Dictionary<string, bool>> quizHistory = quizSerializer.DeserializeQuiz(HistoryQuizFilePath);

            if (quizHistory.Count == 0)
            {
                Console.WriteLine("Не удалось загрузить викторину.");
                Thread.Sleep(1500);
                StartNewGame();
            }

            int correctAnswers = 0;
            int totalQuestions = quizHistory.Count;

            Console.Clear();

            Console.WriteLine("\t\t\t\t╔═══════════════════════════════════════════════════════╗");
            Console.WriteLine("\t\t\t\t║                                                       ║");
            Console.WriteLine("\t\t\t\t║               Добро пожаловать в викторину!           ║");
            Console.WriteLine("\t\t\t\t║                                                       ║");
            Console.WriteLine("\t\t\t\t╠═══════════════════════════════════════════════════════╣");
            Console.WriteLine("\t\t\t\t║                                                       ║");
            Console.WriteLine("\t\t\t\t║             Ответьте на следующие вопросы:            ║");
            Console.WriteLine("\t\t\t\t║                                                       ║");
            Console.WriteLine("\t\t\t\t╚═══════════════════════════════════════════════════════╝");

            Console.Clear();

            ConsoleKeyInfo keyInfo;

            foreach (var question in quizHistory)
            {

                Console.WriteLine("╔══════════════════════════════════════════════════════════════════════════════════════════════════════════════╗");
                Console.WriteLine("║                                                                                                              ");
                Console.WriteLine($"║Вопрос {question.Key}                                                                                        ");
                Console.WriteLine("║                                                                                                              ");
                Console.WriteLine("╚══════════════════════════════════════════════════════════════════════════════════════════════════════════════╝");

                int optionAnswer = 1;
                foreach (var answer in question.Value)
                {
                    Console.WriteLine($"  {optionAnswer}. {answer.Key}");
                    optionAnswer++;
                }

                //ConsoleKeyInfo keyInfo;
                bool validArgument = false;

                while (!validArgument)
                {
                    keyInfo = Console.ReadKey();

                    if (keyInfo.Key >= ConsoleKey.D1 && keyInfo.Key <= ConsoleKey.D4)
                    {
                        validArgument = true;
                        int selectedOptionIndex = keyInfo.Key - ConsoleKey.D1;
                        var selectedOption = question.Value.ElementAt(selectedOptionIndex);
                        if (selectedOption.Value)
                        {
                            correctAnswers++;
                        }
                    }
                    else
                    {
                        Console.WriteLine("Некорректный выбор. Повторите ввод.");
                    }
                }
                //Thread.Sleep(500);
                Console.Clear();
            }

            Console.WriteLine("╔═══════════════════════════════════════════════════════╗");
            Console.WriteLine($"║  Викторина завершена!                                 ║");
            Console.WriteLine($"║  Правильных ответов: {correctAnswers} из {totalQuestions}                           ║");
            Console.WriteLine("╚═══════════════════════════════════════════════════════╝");

            Thread.Sleep(3000);
            Console.Clear();

            Console.WriteLine();
            Console.WriteLine("Для выхода в меню нажмите 1 на клавиатуре, для выхода из программы нажмите 0: ");

            keyInfo = Console.ReadKey();

            if(keyInfo.Key == ConsoleKey.D1)
            {
                MainMenu();
            }
            else
            {
                Environment.Exit(0);
            }
        }

        private void GeographyQuiz()
        {
            // Code...
        }

        private void BiologyQuiz()
        {
            // Code...
        }

        private void LoadingMyQuizz()
        {
            // Code...
        }

        private void StartNewGame()
        {
            Console.Clear();

            Console.WriteLine("\t\t\t\t╔═══════════════════════════════════════════════════════╗");
            Console.WriteLine("\t\t\t\t║                                                       ║");
            Console.WriteLine("\t\t\t\t║                   Выберите тему                       ║");
            Console.WriteLine("\t\t\t\t║                                                       ║");
            Console.WriteLine("\t\t\t\t╠═══════════════════════════════════════════════════════║");
            Console.WriteLine("\t\t\t\t║                                                       ║");
            Console.WriteLine("\t\t\t\t║   1. История                                          ║");
            Console.WriteLine("\t\t\t\t║                                                       ║");
            Console.WriteLine("\t\t\t\t║   2. География                                        ║");
            Console.WriteLine("\t\t\t\t║                                                       ║");
            Console.WriteLine("\t\t\t\t║   3. Биология                                         ║");
            Console.WriteLine("\t\t\t\t║                                                       ║");
            Console.WriteLine("\t\t\t\t║   4. Загрузить свою игру                              ║");
            Console.WriteLine("\t\t\t\t║                                                       ║");
            Console.WriteLine("\t\t\t\t║   5. Назад                                            ║");
            Console.WriteLine("\t\t\t\t║                                                       ║");
            Console.WriteLine("\t\t\t\t╚═══════════════════════════════════════════════════════╝");

            ConsoleKeyInfo keyInfo;

            bool validArgument = false;

            while (!validArgument)
            {
                keyInfo = Console.ReadKey();

                if (keyInfo.Key >= ConsoleKey.D1 && keyInfo.Key <= ConsoleKey.D5)
                {
                    validArgument = true;
                    if (keyInfo.Key == ConsoleKey.D1)
                    {
                        HistoryQuiz();
                    }
                    else if (keyInfo.Key == ConsoleKey.D2)
                    {
                        GeographyQuiz();
                    }
                    else if (keyInfo.Key == ConsoleKey.D3)
                    {
                        BiologyQuiz();
                    }
                    else if (keyInfo.Key == ConsoleKey.D4)
                    {
                        LoadingMyQuizz();
                    }
                    else if (keyInfo.Key == ConsoleKey.D5)
                    {
                        MainMenu();
                    }
                }
                else
                {
                    Console.WriteLine("Некорректный выбор. Повторите ввод.");
                }
            }
        }

        private void SettingsAccount()
        {
            Console.Clear();
            Console.WriteLine("\t\t\t\t╔═══════════════════════════════════════════════════════╗");
            Console.WriteLine("\t\t\t\t║                                                       ║");
            Console.WriteLine("\t\t\t\t║                     Настройки                         ║");
            Console.WriteLine("\t\t\t\t║                                                       ║");
            Console.WriteLine("\t\t\t\t╠═══════════════════════════════════════════════════════║");
            Console.WriteLine("\t\t\t\t║                                                       ║");
            Console.WriteLine("\t\t\t\t║   1. Поменять пароль                                  ║");
            Console.WriteLine("\t\t\t\t║                                                       ║");
            Console.WriteLine("\t\t\t\t║   2. Изменить дату рождения                           ║");
            Console.WriteLine("\t\t\t\t║                                                       ║");
            Console.WriteLine("\t\t\t\t╚═══════════════════════════════════════════════════════╝");

            ConsoleKeyInfo keyInfo;

            bool validArgument = false;

            while(!validArgument)
            {
                keyInfo = Console.ReadKey();

                if(keyInfo.Key >= ConsoleKey.D1 && keyInfo.Key <= ConsoleKey.D2)
                {
                    validArgument = true;
                    if(keyInfo.Key == ConsoleKey.D1)
                    {
                        Console.Clear();
                        Console.WriteLine("\t\t\t\t╔═══════════════════════════════════════════════════════╗");
                        Console.WriteLine("\t\t\t\t║                                                       ║");
                        Console.WriteLine("\t\t\t\t║                     Настройки                         ║");
                        Console.WriteLine("\t\t\t\t║                                                       ║");
                        Console.WriteLine("\t\t\t\t╠═══════════════════════════════════════════════════════║");
                        Console.WriteLine("\t\t\t\t║                                                       ║");
                        Console.WriteLine("\t\t\t\t║   Введите новый пароль:                               ║");
                        Console.WriteLine("\t\t\t\t║                                                       ║");
                        Console.WriteLine("\t\t\t\t╚═══════════════════════════════════════════════════════╝");

                        Console.SetCursorPosition(58, 6);
                        string newPassword = Console.ReadLine();

                        bool isCOrrect = DataCorrectness.isCheckPassword(newPassword);

                        while (!DataCorrectness.isCheckPassword(newPassword))
                        {
                            Console.SetCursorPosition(90, 6);
                            Console.Write("Недопустимый формат пароля");
                            System.Threading.Thread.Sleep(1000);

                            Console.SetCursorPosition(90, 6);
                            Console.Write(new string(' ', "Недопустимый формат пароля".Length));
                            Console.SetCursorPosition(58, 6);
                            Console.Write(new string(' ', newPassword.Length));

                            Console.SetCursorPosition(58, 6);
                            newPassword = Console.ReadLine();
                        }
                        string NewPasswordEncrypt = aesEncryption.Encrypt(newPassword);
                        string json = File.ReadAllText("UserData.json");
                        JArray usersArray = JArray.Parse(json);

                        JObject userToUpdate = usersArray.Children<JObject>().FirstOrDefault(user => user["Login"].ToString() == UserData.Login);

                        if (userToUpdate != null)
                        {
                            userToUpdate["Password"] = NewPasswordEncrypt;
                            string updatedJson = usersArray.ToString();
                            File.WriteAllText("UserData.json", updatedJson);

                            Console.WriteLine("\t\t\t\t╔═══════════════════════════════════════════════════════╗");
                            Console.WriteLine("\t\t\t\t║                                                       ║");
                            Console.WriteLine("\t\t\t\t║               Пароль успешно изменен                  ║");
                            Console.WriteLine("\t\t\t\t║                                                       ║");
                            Console.WriteLine("\t\t\t\t╚═══════════════════════════════════════════════════════╝");
                        }
                        else
                        {
                            Console.WriteLine("Пользователь с указанным логином не найден.");
                        }
                    }
                    else
                    {
                        // Изменить дату
                    }
                }
            }
        }
        private void MainMenu()
        {
            Console.Clear();
            Console.WriteLine("\t\t\t\t╔═══════════════════════════════════════════════════════╗");
            Console.WriteLine("\t\t\t\t║                                                       ║");
            Console.WriteLine("\t\t\t\t║                     Главное меню                      ║");
            Console.WriteLine("\t\t\t\t║                                                       ║");
            Console.WriteLine("\t\t\t\t╠═══════════════════════════════════════════════════════║");
            Console.WriteLine("\t\t\t\t║                                                       ║");
            Console.WriteLine("\t\t\t\t║   1. Новая игра                                       ║");
            Console.WriteLine("\t\t\t\t║                                                       ║");
            Console.WriteLine("\t\t\t\t║   2. Загрузить топ своих викторин                     ║");
            Console.WriteLine("\t\t\t\t║                                                       ║");
            Console.WriteLine("\t\t\t\t║   3. Загрузить топ 20 игроков по викторинам           ║");
            Console.WriteLine("\t\t\t\t║                                                       ║");
            Console.WriteLine("\t\t\t\t║   4. Настройки                                        ║");
            Console.WriteLine("\t\t\t\t║                                                       ║");
            Console.WriteLine("\t\t\t\t║   5. Выход                                            ║");
            Console.WriteLine("\t\t\t\t║                                                       ║");
            Console.WriteLine("\t\t\t\t╚═══════════════════════════════════════════════════════╝");

            ConsoleKeyInfo keyInfo;

            bool validArgument = false;

            while (!validArgument)
            {
                keyInfo = Console.ReadKey();

                if (keyInfo.Key >= ConsoleKey.D1 && keyInfo.Key <= ConsoleKey.D5)
                {
                    validArgument = true;
                    if (keyInfo.Key == ConsoleKey.D1)
                    {
                        StartNewGame();
                    }else if(keyInfo.Key == ConsoleKey.D2)
                    {
                        // ShowTopMyQuizzes();
                    } else if(keyInfo.Key == ConsoleKey.D3)
                    {
                        //ShowTop20PlayersQuizzes();
                    } else if(keyInfo.Key == ConsoleKey.D4)
                    {
                        SettingsAccount();
                    }
                    else
                    {
                        Environment.Exit(0);
                    }
                }
                else
                {
                    Console.WriteLine("Некорректный выбор. Повторите ввод.");
                }
            }
        }

        public void Start()
        {
            AuthorizationForm();
        }
    }
}