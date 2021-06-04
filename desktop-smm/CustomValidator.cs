using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace desktop_smm
{
    public class Validator
    {
        private List<string> errors = new List<string>();

        private Dictionary<string, string> regexDictionary = new Dictionary<string, string>()
        {
            {"CirilicAndSpace", "[^А-ЯЁа-яё ]$" },
            {"Email", @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$" },
            {"Numbers", @"^\d$"},
            {"Text", @"[^a-zA-Z]+"}
        };

        public static bool Check(Dictionary<string, string> array)
        {
            foreach (var field in array)
            {
                Console.WriteLine(field);
            }

            return true;
        }
    }
}
