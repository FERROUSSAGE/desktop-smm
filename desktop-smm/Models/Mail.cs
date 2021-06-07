using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace desktop_smm.Models
{
    public class Mail
    {
        public string toMail { get; set; }
        public string msg { get; set; }
    }

    public class Message
    {
        public string code { get; set; }
        public string command { get; set; }
    }

    public class ErrorMail
    {
        public int status { get; set; }
        public Message message { get; set; }
    }

    public class RootMail
    {
        public bool status { get; set; }
        public Mail response { get; set; }
        public ErrorMail err { get; set; }
    }

}
