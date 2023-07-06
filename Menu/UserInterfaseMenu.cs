using System;
using AesEncryptionNamespace;
using System.Threading;
using DataCorrectnessNamespace;
using System.Collections.Generic;
using AuxiliaryNamespace;
using UserDataNamespace;
using FileManagerNamespace;
using QuizCreatorNamespace;
using AuthenticationManagerNamespace;
using RegistrationNamespace;

namespace UserInterfaseMenuNamespace
{
    public class UserInterfaseMenu
    {
        private readonly AesEncryption aesEncryption = new AesEncryption();
        private readonly AuthenticationManager authenticationManager = new();

        private void HistoryQuiz()
        {
            // Вынести начисление очков в отдельный метод, а лучше вообще запуск викторины в отдельный класс
            string nameQuiz = "History";
            string quizTopic = "История";
            UserData currentUser = authenticationManager.GetCurrectUser();

            if (!QuizCreator.isCheckFile(nameQuiz))
            {
                Console.WriteLine("Не удалось загрузить викторину.");
                StartNewGame();
            }
            QuizCreator.StartQuiz(nameQuiz);

            if (currentUser.QuizResults.TryGetValue(quizTopic, out int existingScore))
            {
                currentUser.QuizResults[quizTopic] = Math.Max(existingScore, QuizCreator.CorrectAnswers);
            }
            else
            {
                currentUser.QuizResults.Add(quizTopic, QuizCreator.CorrectAnswers);
            }

            LoadData.SaveUserDataForUser(currentUser);

            Console.WriteLine("\nДля выхода в меню нажмите 1 на клавиатуре, для выхода на окно авторизации нажмите 0: ");

            ConsoleKeyInfo keyInfo;
            keyInfo = Console.ReadKey();

            if (keyInfo.Key <= ConsoleKey.D1 && keyInfo.Key >= ConsoleKey.D0)
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
        }
        private void GeographyQuiz()
        {
            string nameQuiz = "Geography";
            //string quizTopic = "География";
            UserData currentUser = authenticationManager.GetCurrectUser();

            if (!QuizCreator.isCheckFile(nameQuiz))
            {
                Console.WriteLine("Не удалось загрузить викторину.");
                Thread.Sleep(1000);
                StartNewGame();
            }
            QuizCreator.StartQuiz(nameQuiz);
        }
        private void BiologyQuiz()
        {
            string nameQuiz = "Biology";
            //string quizTopic = "Биология";
            UserData currentUser = authenticationManager.GetCurrectUser();

            if (!QuizCreator.isCheckFile(nameQuiz))
            {
                Console.WriteLine("Не удалось загрузить викторину.");
                Thread.Sleep(1000);
                StartNewGame();
            }
            QuizCreator.StartQuiz(nameQuiz);
        }
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
            Authorization();
        }
        private void Authorization()
        {
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
                        Registration();
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
        private void LoginFormMenu()
        {
            Console.Clear();
            Console.WriteLine("\t\t\t\t╔════════════════════════════════════════════════════════════╗");
            Console.WriteLine("\t\t\t\t║                                                            ║");
            Console.WriteLine("\t\t\t\t║                     Авторизация                            ║");
            Console.WriteLine("\t\t\t\t║                                                            ║");
            Console.WriteLine("\t\t\t\t╠════════════════════════════════════════════════════════════║");
            Console.WriteLine("\t\t\t\t║                                                            ║");
            Console.WriteLine("\t\t\t\t║    Введите логин:                                          ║");
            Console.WriteLine("\t\t\t\t║                                                            ║");
            Console.WriteLine("\t\t\t\t║    Введите пароль:                                         ║");
            Console.WriteLine("\t\t\t\t║                                                            ║");
            Console.WriteLine("\t\t\t\t╚════════════════════════════════════════════════════════════╝");
        }
        private void LoginForm()
        {
            // Вынести авторизацию(вход по логину паролю) в отдельный класс
            Console.Clear();
            bool isCorrectData = false;
            int LoginAttempt = 0;
            string text = string.Empty;
            int cursorPositionInput, cursorNotifyAndInput;

            do
            {
                LoginFormMenu();

                text = "Недопустимый формат логина";
                cursorPositionInput = 52;
                cursorNotifyAndInput = 6;

                Console.SetCursorPosition(cursorPositionInput, cursorNotifyAndInput);
                string loginAuthUser = Console.ReadLine();
                while (!DataCorrectness.isCheckLogin(loginAuthUser))
                {
                    AuxiliaryLog.AuxiliaryLogErrorinput(text, cursorPositionInput, cursorNotifyAndInput);
                    loginAuthUser = Console.ReadLine();
                }

                text = "Недопустимый формат пароля";
                cursorPositionInput = 53;
                cursorNotifyAndInput = 8;
                Console.SetCursorPosition(cursorPositionInput, cursorNotifyAndInput);
                string passwordAuthUser = Console.ReadLine();
                while (!DataCorrectness.isCheckPassword(passwordAuthUser))
                {
                    AuxiliaryLog.AuxiliaryLogErrorinput(text, cursorPositionInput, cursorNotifyAndInput);
                    passwordAuthUser = Console.ReadLine();
                }
                bool isAuthorization = authenticationManager.LoginUser(loginAuthUser, passwordAuthUser);

                if (isAuthorization)
                {
                    isCorrectData = true;
                    Console.Clear();
                    Console.WriteLine("\t\t\t\t╔════════════════════════════════════════════════════════════╗");
                    Console.WriteLine("\t\t\t\t║                                                            ║");
                    Console.WriteLine($"\t\t\t\t║              Авторизация прошла успешно                    ║");
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
        private void Registration()
        {
            using (var registration = new Registration())
            {
                registration.RegistrationSuccess += HandleRegistrationSuccess;
                registration.RegistrationUser();
            }
        }

        private void HandleRegistrationSuccess(object sender, EventArgs e)
        {
            MainMenu();
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
                        //BiologyQuiz();
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
            UserData currentUser = authenticationManager.GetCurrectUser();
            string text = string.Empty;
            int cursorPositionInput, cursorNotifyAndInput;

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
            Console.WriteLine("\t\t\t\t║   3. Вернуться назад                                  ║");
            Console.WriteLine("\t\t\t\t║                                                       ║");
            Console.WriteLine("\t\t\t\t╚═══════════════════════════════════════════════════════╝");

            ConsoleKeyInfo keyInfo;

            bool validArgument = false;

            while(!validArgument)
            {
                keyInfo = Console.ReadKey();

                if(keyInfo.Key >= ConsoleKey.D1 && keyInfo.Key <= ConsoleKey.D3)
                {
                    validArgument = true;
                    if (keyInfo.Key == ConsoleKey.D1)
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
                        cursorNotifyAndInput = 6;

                        Console.SetCursorPosition(cursorPositionInput, cursorNotifyAndInput);
                        string newPassword = Console.ReadLine();

                        while (!DataCorrectness.isCheckPassword(newPassword))
                        {
                            AuxiliaryLog.AuxiliaryLogErrorinput(text, cursorPositionInput, cursorNotifyAndInput);
                            newPassword = Console.ReadLine();
                        }

                        if(currentUser != null)
                        {
                            string NewPasswordEncrypt = aesEncryption.Encrypt(newPassword);
                            currentUser.Password = NewPasswordEncrypt;
                            text = "Пароль был успешно изменен";
                            AuxiliaryLog.NotifyChangeSettings(text);

                            LoadData.SaveUserDataForUser(currentUser);

                            Thread.Sleep(1000);
                            authenticationManager.LogoutUser();
                            LoginForm();
                        }
                        else
                        {
                            text = "Пользователь с указанным логином не найден";
                            AuxiliaryLog.NotifyChangeSettings(text);

                            Thread.Sleep(1000);
                            AuthorizationForm();
                        }
                    }
                    else if (keyInfo.Key == ConsoleKey.D2)
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
                        cursorNotifyAndInput = 6;

                        Console.SetCursorPosition(cursorPositionInput, cursorNotifyAndInput);
                        string newDate = Console.ReadLine();


                        while (!DataCorrectness.IsCheckDate(newDate))
                        {
                            AuxiliaryLog.AuxiliaryLogErrorinput(text, cursorPositionInput, cursorNotifyAndInput);
                            newDate = Console.ReadLine();
                        }

                        DateTime correctData = DataCorrectness.ConvertToDate(newDate);

                        if(currentUser != null)
                        {
                            currentUser.DateOfBirth = correctData;

                            text = "Дата была успешно изменена";
                            AuxiliaryLog.NotifyChangeSettings(text);

                            LoadData.SaveUserDataForUser(currentUser);

                            Thread.Sleep(1000);
                            MainMenu();
                        }
                        else
                        {
                            Console.Clear();
                            Console.WriteLine("\t\t\t\t╔═══════════════════════════════════════════════════════╗");
                            Console.WriteLine("\t\t\t\t║                                                       ║");
                            Console.WriteLine("\t\t\t\t║      Пользователь с указанным логином не найден       ║");
                            Console.WriteLine("\t\t\t\t║                                                       ║");
                            Console.WriteLine("\t\t\t\t╚═══════════════════════════════════════════════════════╝");

                            Thread.Sleep(1000);
                            AuthorizationForm();
                        }
                    } else if(keyInfo.Key == ConsoleKey.D3)
                    {
                        Console.Clear();
                        MainMenu();
                    }
                }
            }
        }
        private void ShowTopMyQuizzes()
        {
            Console.Clear();

            UserData currentUser = authenticationManager.GetCurrectUser();
            Dictionary<string, int> myQuizzResult = new();

            myQuizzResult = currentUser.QuizResults;

            Console.WriteLine("╔══════════════════════════════════════════════════════╗");
            foreach (var item in myQuizzResult)
            {
                Console.WriteLine($"║Тема: {item.Key} ║ Максимальное количество очков: {item.Value}     ║");
            }
            Console.WriteLine("╚══════════════════════════════════════════════════════╝");
            Console.WriteLine("\n Для выхода назад нажмите 1");
            ConsoleKeyInfo keyInfo;

            while (true)
            {
                keyInfo = Console.ReadKey();
                if (keyInfo.Key == ConsoleKey.D1)
                {
                    break;
                }
            }
            MainMenu();

        }
        private void ShowTop20PlayersQuizzes()
        {
            AuthenticationManager authenticationManager = new();
            var AllRegisteredUser = authenticationManager.GetAllRegisteredUser();

            Console.Clear();
            foreach (var user in AllRegisteredUser)
            {
                int totalCountQuizResults = 0;
                foreach (var item in user.QuizResults.Values)
                {
                    totalCountQuizResults += item;
                }
                Console.WriteLine("╔══════════════════════════════════════════════════════╗");
                Console.WriteLine("║                                                      ║");
                Console.WriteLine($"║ Login User: {user.Login}                            ");                         
                Console.WriteLine($"║ Total Score: {totalCountQuizResults}                ");
                Console.WriteLine("║                                                      ║");
                Console.WriteLine("╚══════════════════════════════════════════════════════╝");
            }
            Console.WriteLine("\n\nНажмите 1 чтобы вернуться назад");
            ConsoleKeyInfo keyInfo;
            bool isValidArgument = false;
            while (!isValidArgument)
            {
                keyInfo = Console.ReadKey();
                if (keyInfo.Key == ConsoleKey.D1)
                {
                    isValidArgument = true;
                    MainMenu();
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
            Console.WriteLine("\t\t\t\t║   5. Выход из аккунта                                 ║");
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
                        StartNewGame();
                    else if(keyInfo.Key == ConsoleKey.D2)
                        ShowTopMyQuizzes();
                    else if(keyInfo.Key == ConsoleKey.D3)
                        ShowTop20PlayersQuizzes(); 
                    else if(keyInfo.Key == ConsoleKey.D4)
                        SettingsAccount();
                    else if(keyInfo.Key == ConsoleKey.D5)
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