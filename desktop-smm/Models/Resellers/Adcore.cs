using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace desktop_smm.Models.Resellers
{
    public class Adcore
    {
        public string msg { get; set; }
        public int idProject { get; set; }
    }

    public class RootAdcore
    {
        public bool status { get; set; }
        public Adcore response { get; set; }
    }
}
