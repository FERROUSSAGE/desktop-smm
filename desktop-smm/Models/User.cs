using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace desktop_smm.Models
{
    public class User
    {
        public int id { get; set; }
        public string name { get; set; }
        public string login { get; set; }
        public string password { get; set; }
        public int roleId { get; set; }
    }

    public class ErrorUser
    {
        public int status { get; set; }
        public string message { get; set; }
    }

    public class RootUser
    {
        public bool status { get; set; }
        [JsonProperty("response")]
        public List<User> users { get; set; }
        public ErrorUser err { get; set; }
    }

}
