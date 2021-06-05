using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace desktop_smm.Models
{
    public class ResellerType
    {
        public int id { get; set; }
        public string type { get; set; }
        public double price { get; set; }
        public string description { get; set; }
        public string name { get; set; }
        public int resellerId { get; set; }
        public Reseller reseller { get; set; }
    }

    public class RootResellerType
    {
        public bool status { get; set; }
        [JsonProperty("response")]
        public List<ResellerType> resellerTypes { get; set; }
    }
}
