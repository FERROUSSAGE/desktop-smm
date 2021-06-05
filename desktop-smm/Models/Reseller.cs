using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace desktop_smm.Models
{
    public class Reseller
    {
        public int id { get; set; }
        public string name { get; set; }
        public string api_key { get; set; }
    }

    public class RootReseller
    {
        public bool status { get; set; }
        [JsonProperty("response")]
        public List<Reseller> resellers { get; set; }
    }
}
