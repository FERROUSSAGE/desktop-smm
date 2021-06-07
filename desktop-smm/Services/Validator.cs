using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace desktop_smm.Services
{
    public class Validator
    {
        private static List<string> errors = new List<string>();

        private static Dictionary<string, string> regexDictionary = new Dictionary<string, string>()
        {
            {"Email", @"^(?("")(""[^""]+?""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +  @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9]{2,17}))$" },
            {"Number", "^[0-9]+$"},
            {"Text", "^[А-ЯЁа-яЁ A-Za-z]*$"},
            {"LoginAndPassword", "^[A-Z-a-z0-9]*$"},
            {"Link", @"^http(s)?:\/\/"},
            {"ComboBox", ""}
        };


        private static Dictionary<string, string> patternErrors = new Dictionary<string, string>
        {
            {"Email", "Был введен не корректный E-mail"},
            {"Number", "Был введен не числовой формат данных"},
            {"Text", "Был введен не строковый формат данных"},
            {"Empty", "Была передана пустая строка"},
            {"LoginAndPassword", "Логин и пароль может содержать только латиницу"},
            {"Link", "Была веден формат не в виде ссылки"},
        };

        public static void ShowErrors(List<string> errors)
        {
            foreach (var error in errors)
                MessageBox.Show(error, "Произошла ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
            errors.Clear();
        }
        private static void SelectError(Control control)
        {
            control.BorderBrush = Brushes.Red;
            control.Focus();
        }

        private static void ResetSelect(Control control) => control.BorderBrush = (Brush)Application.Current.Resources["ColorBorder"];

        public static bool ValidateFields(string field)
        {
            if (String.IsNullOrEmpty(field))
                return false;
            return true;
        }

        public static object Check(Dictionary<Control, string> arrays)
        {
            foreach (var field in arrays)
            {
                foreach (var regDic in regexDictionary)
                {
                    if (field.Value == regDic.Key)
                    {
                        var regex = new Regex(regDic.Value, RegexOptions.IgnoreCase | RegexOptions.Compiled);
                        switch (field.Key.GetType().Name)
                        {
                            case "TextBox" :
                                if (!ValidateFields((field.Key as TextBox).Text))
                                {
                                    errors.Add($"{patternErrors["Empty"]}\n\nВ поле - {(field.Key as TextBox).Uid}");
                                    SelectError(field.Key);
                                    return errors;
                                }
                                else
                                {
                                    if (!regex.IsMatch((field.Key as TextBox).Text))
                                    {
                                        errors.Add($"{patternErrors[regDic.Key]}\n\nВ поле - {field.Key.Uid}");
                                        SelectError(field.Key);
                                    }
                                    else ResetSelect(field.Key);
                                } 
                                break;
                            case "ComboBox":
                                if((field.Key as ComboBox).SelectedItem == null)
                                {
                                    if (!ValidateFields(""))
                                    {
                                        errors.Add($"{patternErrors["Empty"]}\n\nВ поле - {(field.Key as ComboBox).Uid}");
                                        SelectError(field.Key);
                                        return errors;
                                    }
                                }
                                else ResetSelect(field.Key);
                                break;
                        }
                    }
                }
            }
            if (errors.Count >= 1)
                return errors;
            else return true;
        }
    }
}