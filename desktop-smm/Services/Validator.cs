using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
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
                {"Text", "^[А-ЯЁа-яЁ A-Za-z]*$"}
            };


        private static Dictionary<string, string> patternErrors = new Dictionary<string, string>
            {
                {"Email", "Был введен не корректный E-mail"},
                {"Numbers", "Был введен не числовой формат данных"},
                {"Text", "Был введен не строковый формат данных"}
            };

        public static void SelectError(Control control)
        {
            control.BorderBrush = Brushes.Red;
            control.Focus();
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
                        switch (field.Key)
                        {
                            case TextBox _:
                                if (!regex.IsMatch((field.Key as TextBox).Text))
                                {
                                    errors.Add(patternErrors[regDic.Key] + " в поле " + field.Key.Uid);
                                    SelectError(field.Key);
                                }
                                break;
                            case ComboBox _:
                                if (!regex.IsMatch((field.Key as ComboBox).SelectedItem.ToString()))
                                {
                                    errors.Add(patternErrors[regDic.Key] + " в поле " + field.Key.Uid);
                                    SelectError(field.Key);
                                }
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