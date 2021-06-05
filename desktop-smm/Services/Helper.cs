using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    }
}
