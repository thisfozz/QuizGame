using System;
using AesEncryptionNamespace;
using System.Threading;
using DataCorrectnessNamespace;
using QuizSerializerNamespace;
using System.Collections.Generic;
using AuxiliaryNamespace;
using UserDataNamespace;
using FileManagerNamespace;
using QuestionQuizNamespace;
using AnswerQuizNamespace;
using QuizCreatorNamespace;
using AuthenticationManagerNamespace;

namespace UserInterfaseMenuNamespace
{
    public class UserInterfaseMenu
    {
        private readonly AuthenticationManagerNamespace.AuthenticationManager authenticationManager = new();
        private readonly AesEncryption aesEncryption = new();

        private void HistoryQuiz()
        {
            string nameQuiz = "History";
            string quizTopic = "История";
            UserData currentUser = authenticationManager.GetCurrectUser();

            if (QuizCreator.StartQuiz(nameQuiz))
            {
                Console.WriteLine("Не удалось загрузить викторину.");
                StartNewGame();
            }

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
            string geographyQuizFilePath = "Geography.json";
            string quizTopic = "География";
            UserData currentUser = authenticationManager.GetCurrectUser();
            QuizSerializer quizSerializer = new QuizSerializer();
            List<QuestionQuiz> quizHistory = quizSerializer.DeserializeQuiz(geographyQuizFilePath);
        }
        private void BiologyQuiz()
        {
            string biologyQuizFilePath = "Biology.json";
            string quizTopic = "Биология";
            UserData currentUser = authenticationManager.GetCurrectUser();
            QuizSerializer quizSerializer = new QuizSerializer();
            List<QuestionQuiz> quizHistory = quizSerializer.DeserializeQuiz(biologyQuizFilePath);
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
            Console.Clear();
            bool isCorrectData = false, isAuthorization = false;
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
                    Auxiliary.AuxiliaryLogErrorinput(text, cursorPositionInput, cursorNotifyAndInput);
                    loginAuthUser = Console.ReadLine();
                }

                text = "Недопустимый формат пароля";
                cursorPositionInput = 53;
                cursorNotifyAndInput = 8;
                Console.SetCursorPosition(cursorPositionInput, cursorNotifyAndInput);
                string passwordAuthUser = Console.ReadLine();
                while (!DataCorrectness.isCheckPassword(passwordAuthUser))
                {
                    Auxiliary.AuxiliaryLogErrorinput(text, cursorPositionInput, cursorNotifyAndInput);
                    passwordAuthUser = Console.ReadLine();
                }
                isAuthorization = authenticationManager.LoginUser(loginAuthUser, passwordAuthUser);


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
        private void RegistrationForm()
        {

            int cursorPositionInput, cursorNotifyAndInput;

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
            cursorNotifyAndInput = 6;
            Console.SetCursorPosition(cursorPositionInput, cursorNotifyAndInput);
            string loginRegistrationUser = Console.ReadLine();
            while (!DataCorrectness.isCheckLogin(loginRegistrationUser))
            {
                Auxiliary.AuxiliaryLogErrorinput(text, cursorPositionInput, cursorNotifyAndInput);
                loginRegistrationUser = Console.ReadLine();
            }


            text = "Недопустимый формат пароля";
            cursorPositionInput = 45;
            cursorNotifyAndInput = 8;
            Console.SetCursorPosition(cursorPositionInput, cursorNotifyAndInput);
            string passwordRegistrationUser = Console.ReadLine();
            while (!DataCorrectness.isCheckPassword(passwordRegistrationUser))
            {
                Auxiliary.AuxiliaryLogErrorinput(text, cursorPositionInput, cursorNotifyAndInput);
                passwordRegistrationUser = Console.ReadLine();
            }
            string passwordEncrypt = aesEncryption.Encrypt(passwordRegistrationUser);


            text = "Недопустимый формат даты";
            cursorPositionInput = 52;
            cursorNotifyAndInput = 10;
            Console.SetCursorPosition(cursorPositionInput, cursorNotifyAndInput);
            string dateBirthRegistrationUser = Console.ReadLine();
            while (!DataCorrectness.IsCheckDate(dateBirthRegistrationUser))
            {
                Auxiliary.AuxiliaryLogErrorinput(text, cursorPositionInput, cursorNotifyAndInput);
                dateBirthRegistrationUser = Console.ReadLine();
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
                        //GeographyQuiz();
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
                            Auxiliary.AuxiliaryLogErrorinput(text, cursorPositionInput, cursorNotifyAndInput);
                            newPassword = Console.ReadLine();
                        }

                        string NewPasswordEncrypt = aesEncryption.Encrypt(newPassword);
                        currentUser.Password = NewPasswordEncrypt;

                        if(currentUser != null)
                        {
                            text = "Пароль был успешно изменен";
                            Auxiliary.NotifyChangeSettings(text);

                            LoadData.SaveUserDataForUser(currentUser);

                            Thread.Sleep(1000);
                            LoginForm();
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
                            Auxiliary.AuxiliaryLogErrorinput(text, cursorPositionInput, cursorNotifyAndInput);
                            newDate = Console.ReadLine();
                        }

                        DateTime correctData = DataCorrectness.ConvertToDate(newDate);

                        if(currentUser != null)
                        {
                            currentUser.DateOfBirth = correctData;

                            text = "Дата была успешно изменена";
                            Auxiliary.NotifyChangeSettings(text);

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
                    }
                }
            }
        }
        private void ShowTopMyQuizzes()
        {
            Console.Clear();
            UserData currentUser = authenticationManager.GetCurrectUser();

            Dictionary<string, int> myQuizzResult = new Dictionary<string, int>();

            myQuizzResult = currentUser.QuizResults;

            Console.WriteLine("╔══════════════════════════════════════════════════════╗");
            foreach (var item in myQuizzResult)
            {
                Console.WriteLine($"║Тема: {item.Key} ║ Максимальное количество очков: {item.Value}      ║");
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
            AuthenticationManager authenticationManager = new AuthenticationManager();
            var AllRegisteredUser = authenticationManager.GetAllRegisteredUser();

            Console.Clear();
            foreach (var user in AllRegisteredUser)
            {
                int totalCountQuizResults = 0;
                foreach (var item in user.QuizResults.Values)
                {
                    totalCountQuizResults += item;
                }
                //Console.Clear();
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