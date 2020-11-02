using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using UPS_APP.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace UPS_APP.Service
{
    public class RestClientService
    {
        private const string URL = "https://gorest.co.in/public-api/";
        private const string Token = "Bearer fa114107311259f5f33e70a5d85de34a2499b4401da069af0b1d835cd5ec0d56";
        public async Task<JObject> GetMethod(string path, string method)
        {
            var response = new HttpResponseMessage();
            var result = new JObject();
            using (var client = new HttpClient())
            {
                var url = URL + path;
                client.DefaultRequestHeaders.Add("Authorization", Token);
                 response = client.GetAsync(url).Result; 
                if(response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    result= JObject.Parse(content);
                }
            }

            return result;
        }

        public async Task<UsersModel> PostMethod(UsersModel user,string path, bool IsPost)
        {
            var response = new HttpResponseMessage();
            var responseUser = new UsersModel();

            if (user == null) return null;
            var data = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");
            using (var client = new HttpClient())
            {
                var url = URL + path;
                client.DefaultRequestHeaders.Add("Authorization", Token);
                response = IsPost == true ? client.PostAsync(url,data ).Result: client.PutAsync(url +"/"+ user.id, data).Result;
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var obj = JObject.Parse(content);
                    if (obj["code"].ToString() != "201") return responseUser;
                    responseUser = JsonConvert.DeserializeObject<UsersModel>(obj["data"].ToString());

                }
            }

            return responseUser;
        }

        public  bool DeleteMethod(string path)
        {
            bool result = false;
            using (var client = new HttpClient())
            {
                var url = URL + path;
                client.DefaultRequestHeaders.Add("Authorization", Token);
                var response = client.DeleteAsync(url).Result;
                if (response.IsSuccessStatusCode)
                {
                    result = true;
                }
                return result;
            }
        }
    }
}
