using System;
using AesEncryptionNamespace;
using UserLoginNamespace;
using UserRegistrationNamespace;
using System.Threading;
using System.Net;

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
            string text = " ";

            do
            {
                Console.WriteLine("\t\t\t\t╔═══════════════════════════════════════════════════════╗");
                Console.WriteLine("\t\t\t\t║                                                       ║");
                Console.WriteLine("\t\t\t\t║                     Авторизация                       ║");
                Console.WriteLine("\t\t\t\t║                                                       ║");
                Console.WriteLine("\t\t\t\t╠═══════════════════════════════════════════════════════║");
                Console.WriteLine("\t\t\t\t║                                                       ║");
                Console.WriteLine("\t\t\t\t║            Введите логин:                             ║");
                Console.WriteLine("\t\t\t\t║                                                       ║");
                Console.WriteLine("\t\t\t\t║            Введите пароль:                            ║");
                Console.WriteLine("\t\t\t\t║                                                       ║");
                Console.WriteLine("\t\t\t\t╚═══════════════════════════════════════════════════════╝");

                Console.SetCursorPosition(60, 6);
                string loginAuthUser = Console.ReadLine();

                Console.SetCursorPosition(61, 8);
                string passwordAuthUser = Console.ReadLine();

                string message = " ";

                isAuthorization = login.LoginUser2(loginAuthUser, passwordAuthUser, out message);

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

                    if (LoginAttempt > 1)
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
            //
        }

        private void AuthorizationMenu()
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


            bool isAuthorization = false, isRegistration = false;

            while (true)
            {
                ConsoleKeyInfo keyInfo = Console.ReadKey();

                if (keyInfo.Key == ConsoleKey.D1)
                {
                    Console.Clear();

                    Console.WriteLine("\t\t\t\t╔═══════════════════════════════════════════════════════╗");
                    Console.WriteLine("\t\t\t\t║                                                       ║");
                    Console.WriteLine("\t\t\t\t║                     Авторизация                       ║");
                    Console.WriteLine("\t\t\t\t║                                                       ║");
                    Console.WriteLine("\t\t\t\t╠═══════════════════════════════════════════════════════║");
                    Console.WriteLine("\t\t\t\t║                                                       ║");
                    Console.WriteLine("\t\t\t\t║            Введите логин:                             ║");
                    Console.WriteLine("\t\t\t\t║                                                       ║");
                    Console.WriteLine("\t\t\t\t║            Введите пароль:                            ║");
                    Console.WriteLine("\t\t\t\t║                                                       ║");
                    Console.WriteLine("\t\t\t\t╚═══════════════════════════════════════════════════════╝");

                    Console.SetCursorPosition(60, 6);
                    string loginUser = Console.ReadLine();

                    Console.SetCursorPosition(61, 8);
                    string passwordUser = Console.ReadLine();

                    string message = " ";

                    isAuthorization = login.LoginUser2(loginUser, passwordUser, out message);
                    if (isAuthorization)
                    {
                        Console.Clear();
                        Console.WriteLine("\t\t\t\t ╔═══════════════════════════════════════════════════════╗");
                        Console.WriteLine("\t\t\t\t ║                                                       ║");
                        Console.WriteLine($"\t\t\t\t ║                {message}                 ║");
                        Console.WriteLine("\t\t\t\t ║                                                       ║");
                        Console.WriteLine("\t\t\t\t ╚═══════════════════════════════════════════════════════╝");
                        Thread.Sleep(2000);
                        Console.Clear();
                        MainMenu();
                        break;
                    }
                    else if (isAuthorization == false)
                    {
                        Console.Clear();
                        Console.WriteLine("\t\t\t\t╔═══════════════════════════════════════════════════════╗");
                        Console.WriteLine("\t\t\t\t║                                                       ║");
                        Console.WriteLine($"\t\t\t\t║              {message}                  ║");
                        Console.WriteLine("\t\t\t\t║                                                       ║");
                        Console.WriteLine("\t\t\t\t╚═══════════════════════════════════════════════════════╝");
                        Thread.Sleep(1000);
                        Console.ReadKey();
                        Console.Clear();

                        continue;
                    }
                }
                else if (keyInfo.Key == ConsoleKey.D2)
                {
                    Console.Clear();

                    Console.WriteLine("Регистрация пользователя");

                    Console.Write("Введите логин: ");
                    string loginUser = Console.ReadLine();

                    Console.Write("Введите пароль: ");
                    string passwordUser = Console.ReadLine();

                    Console.WriteLine("Введите дату рождения (дд-мм-гггг): ");
                    DateTime birthdate = DateTime.Parse(Console.ReadLine());

                    isRegistration = registration.Register(loginUser, passwordUser, birthdate);

                    if (isRegistration)
                    {
                        Console.WriteLine("Регистрация прошла успешно");
                        MainMenu();
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Пользователь с таким логином уже зарегистрирован");
                    }
                }
            }
        }



        private void MainMenu()
        {

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