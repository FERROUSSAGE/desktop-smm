using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace desktop_smm.Models.Resellers
{
    public class Spanel
    {
        public string error { get; set; }
        public int idProject { get; set; }
        public double spend { get; set; }
    }

    public class RootSpanel
    {
        public bool status { get; set; }
        public Spanel response { get; set; }
    }

}
