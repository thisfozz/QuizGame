using System;
using System.Threading;

namespace AuxiliaryNamespace
{
    public class Auxiliary
    {
        public static void AuxiliaryLog(string errorMessage, int cursorPositionInput, int cursorNotifyAndInput)
        {
            int errorLength = errorMessage.Length;
            Console.SetCursorPosition(cursorPositionInput, cursorNotifyAndInput);
            Console.Write(new string(' ', errorLength));
            Console.SetCursorPosition(cursorPositionInput, cursorNotifyAndInput);
            Console.Write(errorMessage);
            Thread.Sleep(1000);
            Console.SetCursorPosition(cursorPositionInput, cursorNotifyAndInput);
            Console.Write(new string(' ', errorLength));
            Console.SetCursorPosition(cursorPositionInput, cursorNotifyAndInput);
        }
    }
}