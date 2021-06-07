using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace desktop_smm.Models.Resellers
{
    public class Smmok
    {
        public string id { get; set; }
        public double balance { get; set; }
        public string msg { get; set; }
    }

    public class RootSmmok
    {
        public bool status { get; set; }
        public Smmok response { get; set; }
    }
}
