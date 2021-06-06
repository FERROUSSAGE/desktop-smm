using desktop_smm.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace desktop_smm.Services
{
    public class Store
    {
        public static List<Order> orders = new List<Order>();
        public static List<ResellerType> resellerTypes = new List<ResellerType>();
        public static List<Reseller> resellers = new List<Reseller>();

        public static User user = null;
        public static int countPage = 0;
        public static int lastPage = 0;

        public async static Task FetchOrders(string page = "1")
        {
            var request = new Request("orders", page);
            var result = await request.GetApiData<RootOrder>();
            orders = result.response.orders;

            countPage = result.response.count;
            lastPage = (int)Math.Ceiling((decimal)result.response.count / 10);
        }

        public async static Task FetchTypes()
        {
            var request = new Request("type", "");
            var result = await request.GetApiData<RootResellerType>();
            resellerTypes = result.resellerTypes;
        }
        public async static Task FetchResellers()
        {
            var request = new Request("reseller", "");
            var result = await request.GetApiData<RootReseller>();
            resellers = result.resellers;
        }
    }
}
