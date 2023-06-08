using System;
using AesEncryptionNamespace;
using UserLoginNamespace;
using UserRegistrationNamespace;
using System.Threading;
using System.Net;
using DataCorrectnessNamespace;

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

            ConsoleKeyInfo keyInfo = Console.ReadKey();

            if (keyInfo.Key == ConsoleKey.D1)
            {
                LoginForm();
            }
            else if(keyInfo.Key == ConsoleKey.D2)
            {
                RegistrationForm();
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
                    System.Threading.Thread.Sleep(1000); // Приостановка на 1 секунду

                    // Очистить сообщение об ошибке и поле ввода
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
                    System.Threading.Thread.Sleep(1000); // Приостановка на 1 секунду

                    // Очистить сообщение об ошибке и поле ввода
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
                    Console.WriteLine("\t\t\t\t ╔═══════════════════════════════════════════════════════╗");
                    Console.WriteLine("\t\t\t\t ║                                                       ║");
                    Console.WriteLine($"\t\t\t\t ║                {message}                 ║");
                    Console.WriteLine("\t\t\t\t ║                                                       ║");
                    Console.WriteLine("\t\t\t\t ╚═══════════════════════════════════════════════════════╝");

                    Thread.Sleep(2000);

                    Console.Clear();

                    MainMenu();
                }
                else
                {
                    LoginAttempt++;
                    Console.Clear();
                    Console.WriteLine("\t\t\t\t╔═══════════════════════════════════════════════════════╗");
                    Console.WriteLine("\t\t\t\t║                                                       ║");
                    Console.WriteLine($"\t\t\t\t║              {message}                  ║");
                    Console.WriteLine("\t\t\t\t║                                                       ║");
                    Console.WriteLine("\t\t\t\t╚═══════════════════════════════════════════════════════╝");
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
                System.Threading.Thread.Sleep(1000); // Приостановка на 1 секунду

                // Очистить сообщение об ошибке и поле ввода
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
                System.Threading.Thread.Sleep(1000); // Приостановка на 1 секунду

                // Очистить сообщение об ошибке и поле ввода
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
        }
        public void Start()
        {
            AuthorizationForm();
        }
    }
}