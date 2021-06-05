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

        public async static Task FetchOrders()
        {
            var request = new Request("order", "");
            var result = await request.GetApiData<RootOrder>();
            orders = result.response.orders;
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
