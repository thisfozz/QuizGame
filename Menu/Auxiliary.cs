using DataCorrectnessNamespace;
using System;
using System.Threading;
using static System.Net.Mime.MediaTypeNames;

namespace AuxiliaryNamespace
{
    public class Auxiliary
    {
        // cursorPositionInput - позиция курсора куда будет печататься текст
        // CursorPositionNotify - позиция курсора куда будет приходить Notify - textInCOnsole
        // cursorNotifyAndInput - номер строки куда смотреть курсору cursorPositionInput
        // userData - то что использует юзер


        // Переписать
        public static bool AuxiliaryLog(string textInCOnsole, string userData, int cursorPositionInput, int CursorPositionNotify, int cursorNotifyAndInput)
        {
            Console.SetCursorPosition(CursorPositionNotify, cursorNotifyAndInput);
            Console.Write(textInCOnsole);
            Thread.Sleep(1000);

            Console.SetCursorPosition(CursorPositionNotify, cursorNotifyAndInput);
            Console.Write(new string(' ', "Недопустимый формат логина".Length));
            Console.SetCursorPosition(cursorPositionInput, cursorNotifyAndInput);
            Console.Write(new string(' ', userData.Length));

            return true;
        }

    }
}
