using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace desktop_smm.Models
{
    public class Streambooster
    {
        public string msg { get; set; }
        public string uuid { get; set; }
    }

    public class RootStreambooster
    {
        public bool status { get; set; }
        public Streambooster response { get; set; }
    }

}
