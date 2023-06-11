using System;
using AesEncryptionNamespace;
using System.Threading;
using DataCorrectnessNamespace;
using QuizSerializerNamespace;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using System.IO;
using AuxiliaryNamespace;

namespace UserInterfaseMenuNamespace
{
    public class UserInterfaseMenu
    {
        private AuthenticationManagerNamespace.AuthenticationManager authenticationManager = new AuthenticationManagerNamespace.AuthenticationManager();
        private AesEncryption aesEncryption = new AesEncryption();

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
                Console.Clear();
            }

            Console.WriteLine("╔════════════════════════════════════════════════════════════════════════════════════════════");
            Console.WriteLine("║                                                                                            ");
            Console.WriteLine("║  Викторина завершена!                                                                      ");
            Console.WriteLine("║                                                                                            ");
            Console.WriteLine($"║ Правильных ответов: {correctAnswers} из {totalQuestions}");
            Console.WriteLine("║                                                                                            ");
            Console.WriteLine("╚════════════════════════════════════════════════════════════════════════════════════════════");

            Thread.Sleep(3000);
            Console.Clear();

            Console.WriteLine();
            Console.WriteLine("Для выхода в меню нажмите 1 на клавиатуре, для выхода на окно авторизации нажмите 0: ");

            keyInfo = Console.ReadKey();

            if(keyInfo.Key <= ConsoleKey.D1 && keyInfo.Key >= ConsoleKey.D0)
            {
                if (keyInfo.Key == ConsoleKey.D1)
                {
                    MainMenu();
                }
                else
                {
                    AuthorizationForm();
                }
            }
            else
            {
                Console.WriteLine("Некорректный ввод");
            }
        }

        // Дописать
        private void GeographyQuiz()
        {
            // Code...
        }

        // Дописать
        private void BiologyQuiz()
        {
            // Code...
        }

        // Дописать
        private void LoadingMyQuizz()
        {
            // Code...
        }

        private void AuthorizationForm()
        {
            Console.Clear();
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
            Console.WriteLine("\t\t\t\t║   3. Выйти из приложения                              ║");
            Console.WriteLine("\t\t\t\t║                                                       ║");
            Console.WriteLine("\t\t\t\t╚═══════════════════════════════════════════════════════╝");

            ConsoleKeyInfo keyInfo;

            bool validArgument = false;
            while (!validArgument)
            {
                keyInfo = Console.ReadKey();
                if (keyInfo.Key >= ConsoleKey.D1 && keyInfo.Key <= ConsoleKey.D3)
                {
                    if (keyInfo.Key == ConsoleKey.D1)
                    {
                        LoginForm();
                    }
                    else if (keyInfo.Key == ConsoleKey.D2)
                    {
                        RegistrationForm();
                    }
                    else
                    {
                        Environment.Exit(0);
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
            int cursorPositionInput, CursorPositionNotify, cursorNotifyAndInput;

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

                text = "Недопустимый формат логина";
                cursorPositionInput = 52;
                CursorPositionNotify = 90;
                cursorNotifyAndInput = 6;

                Console.SetCursorPosition(cursorPositionInput, cursorNotifyAndInput);
                string loginAuthUser = Console.ReadLine();
                while (!DataCorrectness.isCheckLogin(loginAuthUser))
                {
                    //if(!Auxiliary.AuxiliaryLog(text, loginAuthUser, cursorPositionInput, CursorPositionNotify, cursorNotifyAndInput));
                }


                text = "Недопустимый формат пароля";
                cursorPositionInput = 53;
                CursorPositionNotify = 90;
                cursorNotifyAndInput = 8;
                Console.SetCursorPosition(cursorPositionInput, cursorNotifyAndInput);
                string passwordAuthUser = Console.ReadLine();
                while (!DataCorrectness.isCheckPassword(passwordAuthUser))
                {
                    //Auxiliary.AuxiliaryLog(text, passwordAuthUser, cursorPositionInput, CursorPositionNotify, cursorNotifyAndInput);
                }
                isAuthorization = authenticationManager.LoginUser(loginAuthUser, passwordAuthUser);


                if (isAuthorization)
                {
                    isCorrectData = true;
                    Console.Clear();
                    Console.WriteLine("\t\t\t\t╔════════════════════════════════════════════════════════════╗");
                    Console.WriteLine("\t\t\t\t║                                                            ║");
                    Console.WriteLine($"\t\t\t\t║           Авторизация прошла успешно                       ║");
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
                    Console.WriteLine($"\t\t\t\t║               Неверный логин или пароль                   ║");
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
                        text = text.ToLower();

                        if (text == "yes")
                        {
                            isCorrectData = true;
                            Console.Clear();
                            AuthorizationForm();
                        }
                        else if (text == "no")
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

            int cursorPositionInput, CursorPositionNotify, cursorNotifyAndInput;

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

            text = "Недопустимый формат логина";
            cursorPositionInput = 44;
            CursorPositionNotify = 90;
            cursorNotifyAndInput = 6;
            Console.SetCursorPosition(cursorPositionInput, cursorNotifyAndInput);
            string loginRegistrationUser = Console.ReadLine();
            while (!DataCorrectness.isCheckLogin(loginRegistrationUser))
            {
                Auxiliary.AuxiliaryLog(text, loginRegistrationUser, cursorPositionInput, CursorPositionNotify, cursorNotifyAndInput);
            }


            text = "Недопустимый формат пароля";
            cursorPositionInput = 45;
            CursorPositionNotify = 90;
            cursorNotifyAndInput = 8;
            Console.SetCursorPosition(cursorPositionInput, cursorNotifyAndInput);
            string passwordRegistrationUser = Console.ReadLine();
            while (!DataCorrectness.isCheckPassword(passwordRegistrationUser))
            {
                //Auxiliary.AuxiliaryLog(text, passwordRegistrationUser, cursorPositionInput, CursorPositionNotify, cursorNotifyAndInput);
            }
            string passwordEncrypt = aesEncryption.Encrypt(passwordRegistrationUser);


            text = "Недопустимый формат даты";
            cursorPositionInput = 52;
            CursorPositionNotify = 90;
            cursorNotifyAndInput = 10;
            Console.SetCursorPosition(cursorPositionInput, cursorNotifyAndInput);
            string dateBirthRegistrationUser = Console.ReadLine();
            while (!DataCorrectness.IsCheckDate(dateBirthRegistrationUser))
            {
                Auxiliary.AuxiliaryLog(text, passwordRegistrationUser, cursorPositionInput, CursorPositionNotify, cursorNotifyAndInput);
            }
            DateTime correctData = DataCorrectness.ConvertToDate(dateBirthRegistrationUser);
            isRegistration = authenticationManager.RegisterUser(loginRegistrationUser, passwordEncrypt, correctData);

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
            string text = string.Empty;
            int cursorPositionInput, CursorPositionNotify, cursorNotifyAndInput;

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


                        text = "Недопустимый формат пароля";
                        cursorPositionInput = 58;
                        CursorPositionNotify = 90;
                        cursorNotifyAndInput = 6;

                        Console.SetCursorPosition(cursorPositionInput, cursorNotifyAndInput);
                        string newPassword = Console.ReadLine();

                        bool isCOrrect = DataCorrectness.isCheckPassword(newPassword);

                        while (!DataCorrectness.isCheckPassword(newPassword))
                        {
                            Auxiliary.AuxiliaryLog(text, newPassword, cursorPositionInput, CursorPositionNotify, cursorNotifyAndInput);
                        }
                        string NewPasswordEncrypt = aesEncryption.Encrypt(newPassword);
                        string json = File.ReadAllText("UserData.json");
                        JArray usersArray = JArray.Parse(json);

                        JObject userToUpdate = usersArray.Children<JObject>().FirstOrDefault(user => user["Login"].ToString() == authenticationManager.GetLoggedInUserLogin());

                        if (userToUpdate != null)
                        {
                            userToUpdate["Password"] = NewPasswordEncrypt;
                            string updatedJson = usersArray.ToString();
                            File.WriteAllText("UserData.json", updatedJson);

                            Console.Clear();
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
                        Console.Clear();
                        Console.WriteLine("\t\t\t\t╔═══════════════════════════════════════════════════════╗");
                        Console.WriteLine("\t\t\t\t║                                                       ║");
                        Console.WriteLine("\t\t\t\t║                     Настройки                         ║");
                        Console.WriteLine("\t\t\t\t║                                                       ║");
                        Console.WriteLine("\t\t\t\t╠═══════════════════════════════════════════════════════║");
                        Console.WriteLine("\t\t\t\t║                                                       ║");
                        Console.WriteLine("\t\t\t\t║   Введите новую дату:                                 ║");
                        Console.WriteLine("\t\t\t\t║                                                       ║");
                        Console.WriteLine("\t\t\t\t╚═══════════════════════════════════════════════════════╝");


                        text = "Недопустимый формат даты";
                        cursorPositionInput = 58;
                        CursorPositionNotify = 90;
                        cursorNotifyAndInput = 6;

                        Console.SetCursorPosition(cursorPositionInput, cursorNotifyAndInput);
                        string newDate = Console.ReadLine();

                        bool isCOrrect = DataCorrectness.IsCheckDate(newDate);

                        while (!DataCorrectness.IsCheckDate(newDate))
                        {
                            Auxiliary.AuxiliaryLog(text, newDate, cursorPositionInput, CursorPositionNotify, cursorNotifyAndInput);
                        }
                        DateTime correctData = DataCorrectness.ConvertToDate(newDate);

                        string json = File.ReadAllText("UserData.json");
                        JArray usersArray = JArray.Parse(json);
                        JObject userToUpdate = usersArray.Children<JObject>().FirstOrDefault(user => user["Login"].ToString() == authenticationManager.GetLoggedInUserLogin());

                        if (userToUpdate != null)
                        {
                            userToUpdate["DateOfBirth"] = correctData;
                            string updatedJson = usersArray.ToString();
                            File.WriteAllText("UserData.json", updatedJson);

                            Console.Clear();
                            Console.WriteLine("\t\t\t\t╔═══════════════════════════════════════════════════════╗");
                            Console.WriteLine("\t\t\t\t║                                                       ║");
                            Console.WriteLine("\t\t\t\t║               Дата успешно изменена                   ║");
                            Console.WriteLine("\t\t\t\t║                                                       ║");
                            Console.WriteLine("\t\t\t\t╚═══════════════════════════════════════════════════════╝");
                        }
                        else
                        {
                            Console.WriteLine("Пользователь с указанным логином не найден.");
                        }
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
                        authenticationManager.LogoutUser();
                        AuthorizationForm();
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