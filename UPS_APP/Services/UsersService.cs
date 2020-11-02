using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UPS_APP.IService;
using UPS_APP.Model;

namespace UPS_APP.Service
{
    public class UsersService : RestClientService, IUsersService
    {
        RestClientService _client = new RestClientService();
        public List<UsersModel> LoadUsers()
        {
            var json =  _client.GetMethod("users", "GET").Result;
            return JsonConvert.DeserializeObject<List<UsersModel>>(json["data"].ToString());
        }

        public UsersModel AddUser(UsersModel user)
        {
            return _client.PostMethod(user, "users", true).Result;
        }

        public UsersModel Update(UsersModel user)
        {
            return _client.PostMethod(user, "users", false).Result;
        }
        public List<UsersModel> SearchByName(string name)
        {
            var json = _client.GetMethod("users?first_name=" + name, "GET").Result;
            return JsonConvert.DeserializeObject<List<UsersModel>>(json["data"].ToString());
        }
        public UsersModel SearchById(int Id)
        {
            var json = _client.GetMethod("users/" + Id, "GET").Result;
            return JsonConvert.DeserializeObject<UsersModel>(json["data"].ToString());
        }
        public bool Delete( int Id)
        {
            return _client.DeleteMethod("users/" + Id);
        }
    }
}
