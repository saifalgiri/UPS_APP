using UPS_APP.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UPS_APP.IService
{
    public interface  IUsersService
    {
        List<UsersModel> LoadUsers();
        UsersModel AddUser(UsersModel user);
        UsersModel Update(UsersModel user);
        bool Delete(int Id);
        List<UsersModel> SearchByName(string name);
        UsersModel SearchById(int Id);
    }
}
