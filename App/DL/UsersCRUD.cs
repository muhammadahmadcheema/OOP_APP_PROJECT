using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App.BL;

namespace App.DL
{
    class UsersCRUD
    {
        public static void AddUser(string username, string password, string role)
        {
            Users newUser = new Users(username, password, role);
            BL.Users.users.Add(newUser);
            UI.Interface.WriteData(UI.Interface.path, BL.Users.users);
        }

        
    }
}
