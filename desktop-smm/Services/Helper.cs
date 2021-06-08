using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using desktop_smm.Models;

namespace desktop_smm.Services
{
    public class ItemForCombobox
    {
        public string name { get; set; }
        public string description { get; set; } = null;
        public string type { get; set; } = null;
    }
    public class Helper
    {
        public static List<ItemForCombobox> SetItemsForCombobox(List<ResellerType> types)
        {
            List<ItemForCombobox> items = new List<ItemForCombobox>();

            foreach (var type in types as List<ResellerType>)
            {
                items.Add(new ItemForCombobox
                {
                    name = type.name,
                    description = type.description,
                    type = type.type
                });
            }

            return items;
        }

        public static List<string> SetPaymentsForCombobox()
        {
            return new List<string>
            {
                "RK", "UP", "WM", "PP", "ИК", "FK"
            };
        }

        public static List<string> SetSocialNetworksForCombobox()
        {
            return new List<string>
            {
                "YouTube", "Instagram", "Вконтакте", "Twitch", "Facebook",
                "TikTok", "Twitter", "Одноклассники", "Telegram", "Likee",
                "SoundCloud", "Spotify"
            };
        }

        public static string Rounded(double digit) => string.Join(".", Math.Round(digit, 2).ToString().Split(','));

        public static void ClearFields(Control[] controls)
        {
            foreach (var control in controls)
            {
                if (control.GetType().Name == "TextBox")
                    (control as TextBox).Text = string.Empty;
                if (control.GetType().Name == "ComboBox")
                    (control as ComboBox).Text = (control as ComboBox).Uid;
            }
        }

        public async static Task<string> GetBalance(string service)
        {
            var request = new Request(service, "balance");
            var response = await request.GetApiData<RootBalance>();
            if (response.status)
                return response.response.balance;
            return "0";
        }
    }
}
