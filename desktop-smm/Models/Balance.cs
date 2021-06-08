using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace desktop_smm.Models
{
    public class Balance
    {
        public string balance { get; set; }
    }

    public class RootBalance
    {
        public bool status { get; set; }
        public Balance response { get; set; }
    }
}
