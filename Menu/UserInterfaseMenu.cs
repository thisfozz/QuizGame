using System;
using AesEncryptionNamespace;
using UserLoginNamespace;
using UserRegistrationNamespace;
using System.Threading;

namespace UserInterfaseMenuNamespace
{
    internal class UserInterfaseMenu
    {
        private UserRegistration registration = new UserRegistration();
        private Login login = new Login();
        private AesEncryption aesEncryption = new AesEncryption();
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

            while (true)
            {
                ConsoleKeyInfo keyInfo = Console.ReadKey();
                if(keyInfo.Key == ConsoleKey.D1)
                {
                    login.LoginUser();
                    break;
                }else if(keyInfo.Key == ConsoleKey.D2)
                {
                    registration.Register();
                    break;
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
            AuthorizationMenu();
        }
    }
}
