using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace desktop_smm.Models.Resellers
{
    public class ResellerInfo
    {
        public int performed { get; set; }
        public string total { get; set; }
        public string msg { get; set; }
    }

    public class RootResellerInfo
    {
        public bool status { get; set; }
        public ResellerInfo response { get; set; }
    }
}
