using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace desktop_smm.Models
{
    public class Spreadsheet
    {
        public string msg { get; set; }
    }

    public class RootSpreadsheet
    {
        public bool status { get; set; }
        public Spreadsheet response { get; set; }
    }
}
