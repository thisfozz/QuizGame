﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DataCorrectnessNamespace
{
    public class DataCorrectness
    {
        public static bool isCheckLogin(string loginUser)
        {
            string patternIsCyrillic = @"^[a-zA-Z0-9]";
            if (string.IsNullOrEmpty(loginUser)) return false;
            if(!Regex.IsMatch(loginUser, patternIsCyrillic)) return false;

            return true;
        }
        public static bool isCheckPassword(string passwordUser)
        {
            if (string.IsNullOrEmpty(passwordUser)) return false;
            if (Regex.IsMatch(passwordUser, @"\p{IsCyrillic}") || passwordUser.Length < 8) return false;

            return true;
        }
        public static bool IsCheckDate(string date)
        {
            if (string.IsNullOrEmpty(date))
                return false;

            if (!DateTime.TryParseExact(date, "dd.MM.yyyy", null, DateTimeStyles.None, out DateTime parsedDate))
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public static DateTime ConvertToDate(string date)
        {
            DateTime convertedDate;
            if (DateTime.TryParseExact(date, "dd.MM.yyyy", null, DateTimeStyles.None, out convertedDate))
            {
                return convertedDate;
            }
            else
            {
                // В случае неверного формата строки или ошибки при преобразовании,
                // можно выбрать возвращаемое значение по умолчанию или бросить исключение.
                // Ниже пример выбора возвращаемого значения по умолчанию:
                return DateTime.MinValue;
            }
        }
    }
}
