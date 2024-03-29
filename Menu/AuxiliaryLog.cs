﻿using System;
using System.Threading;

namespace AuxiliaryNamespace
{
    public abstract class AuxiliaryLog
    {
        public static void AuxiliaryLogErrorinput(string errorMessage, int cursorPositionInput, int cursorNotifyAndInput)
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

        public static void NotifyChangeSettings(string message)
        {
            Console.Clear();
            Console.WriteLine("\t\t\t\t╔═══════════════════════════════════════════════════════╗");
            Console.WriteLine("\t\t\t\t║                                                       ║");
            Console.WriteLine($"\t\t\t\t║               {message}              ║");
            Console.WriteLine("\t\t\t\t║                                                       ║");
            Console.WriteLine("\t\t\t\t╚═══════════════════════════════════════════════════════╝");
        }
    }
}