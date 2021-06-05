using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace desktop_smm.Services
{
    public class Validator
    {
        private static List<string> errors = new List<string>();

        private static Dictionary<string, string> regexDictionary = new Dictionary<string, string>()
        {
            {"Email", @"^(?("")(""[^""]+?""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +  @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9]{2,17}))$" },
            {"Numbers", "^[0-9]+$"},
            {"Text", "^[А-ЯЁа-яЁ A-Za-z]*$"},
            {"LoginAndPassword", "^[A-Z-a-z0-9]*$"}
        };


        private static Dictionary<string, string> patternErrors = new Dictionary<string, string>
        {
            {"Email", "Был введен не корректный E-mail"},
            {"Numbers", "Был введен не числовой формат данных"},
            {"Text", "Был введен не строковый формат данных"},
            {"Empty", "Была передана пустая строка"},
            {"LoginAndPassword", "Логин и пароль может содержать только латиницу"}
        };

        public static bool ValidateFields(string[] fields)
        {
            foreach(var field in fields)
                if (String.IsNullOrEmpty(field))
                    return false;
            return true;
        }

        public static void ShowErrors(List<string> errors)
        {
            foreach (var error in errors)
                MessageBox.Show(error, "Произошла ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
            errors.Clear();
        }

        public static object Check(Dictionary<char[], string> arrays)
        {
            List<string> fields = new List<string>();
            foreach (var key in arrays.Keys) fields.Add(new string(key));

            if (!ValidateFields(fields.ToArray())) { errors.Add(patternErrors["Empty"]); return errors; }

            foreach (var item in arrays)
            {
                foreach (var regDic in regexDictionary)
                {
                    if (item.Value == regDic.Key)
                        if (!new Regex(regDic.Value, RegexOptions.IgnoreCase | RegexOptions.Compiled).IsMatch(new string(item.Key)))
                            errors.Add(patternErrors[regDic.Key]);
                }
            }

            if (errors.Count >= 1)
                return errors;
            else return true;
        }
    }
}