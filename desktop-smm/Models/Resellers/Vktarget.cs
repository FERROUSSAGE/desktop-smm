using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace desktop_smm.Models.Resellers
{
    public class Vktarget
    {
        public string msg { get; set; }
        public int id { get; set; }
    }

    public class RootVktarget
    {
        public bool status { get; set; }
        public Vktarget response { get; set; }
    }

}
