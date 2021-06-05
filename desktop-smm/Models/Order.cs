using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace desktop_smm.Models
{
    public class Order
    {
        public int id { get; set; }
        public int idSmmcraft { get; set; }
        public int? idProject { get; set; }
        public string socialNetwork { get; set; }
        public string link { get; set; }
        public int cost { get; set; }
        public double spend { get; set; }
        public int countOrdered { get; set; }
        public int countViews { get; set; }
        public string payment { get; set; }
        public DateTime dateCreate { get; set; }
        public string date { get; set; }
        public int? resellerId { get; set; }
        public int? resellerTypeId { get; set; }
        public int userId { get; set; }
        public User user { get; set; }
        public Reseller reseller { get; set; }
        public ResellerType reseller_type { get; set; }
    }

    public class Response
    {
        public int count { get; set; }

        [JsonProperty("rows")]
        public List<Order> orders { get; set; }
    }

    public class RootOrder
    {
        public bool status { get; set; }
        public Response response { get; set; }
    }
}

