using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace desktop_smm.Services
{
    public class Request
    {
        private readonly HttpClient client = new HttpClient();

        private readonly string URL = ConfigurationManager.AppSettings["APP_API_URL"];
        private readonly Dictionary<string, string> POSTDATA;

        public Request(string mainParam, string method, Dictionary<string, string> data = null)
        {
            this.POSTDATA = data;

            switch (mainParam)
            {
                case "orders":
                    this.URL += $"order?page={method}";
                    break;
                case "order":
                    this.URL += $"order/{method}";
                    break;
                case "checkOrder":
                    this.URL += $"order/{method}";
                    break;
                case "orderByText":
                    this.URL += $"order/{method}";
                    break;
                case "type":
                    this.URL += $"type/{method}";
                    break;
                case "reseller":
                    this.URL += $"reseller/{method}";
                    break;
                case "spreadsheet":
                    this.URL += $"google/spreadsheet";
                    break;
                case "nodemailer":
                    this.URL += $"google/nodemailer";
                    break;
                case "adcore":
                    this.URL += $"api/adcore/{method}";
                    break;
                case "smmok":
                    this.URL += $"api/smmok/{method}";
                    break;
                case "vktarget":
                    this.URL += $"api/vktarget/{method}";
                    break;
                case "spanel":
                    this.URL += $"api/spanel/{method}";
                    break;
                case "user":
                    this.URL += $"user/{method}";
                    break;
                case "streambooster":
                    this.URL += $"streambooster/{method}";
                    break;
            }
        }

        public async Task<T> PostAPIData<T>()
        {
            var json = JsonConvert.SerializeObject(POSTDATA);

            using (HttpResponseMessage responseMessage = await client.PostAsync(URL, new StringContent(json, Encoding.UTF8, "application/json")).ConfigureAwait(false))
            {
                string jsonText = await responseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
                return JsonConvert.DeserializeObject<T>(jsonText);
            }
        }

        public async Task<T> PutAPIData<T>()
        {
            var json = JsonConvert.SerializeObject(POSTDATA);

            using (HttpResponseMessage responseMessage = await client.PutAsync(URL, new StringContent(json, Encoding.UTF8, "application/json")).ConfigureAwait(false))
            {
                string jsonText = await responseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
                return JsonConvert.DeserializeObject<T>(jsonText);
            }
        }
        public async Task<T> DeleteAPIData<T>()
        {
            using (HttpResponseMessage responseMessage = await client.DeleteAsync(URL).ConfigureAwait(false))
            {
                string jsonText = await responseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
                return JsonConvert.DeserializeObject<T>(jsonText);
            }
        }
        public async Task<T> GetApiData<T>()
        {
            using (HttpResponseMessage responseMessage = await client.GetAsync(URL).ConfigureAwait(false))
            {
                string jsonText = await responseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
                return JsonConvert.DeserializeObject<T>(jsonText);
            }
        }
    }
}
