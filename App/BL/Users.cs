using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.BL
{
    class Users
    {
        public string username { get; set; }
        public string password { get; set; }
        public string role { get; set; }

        public Users(string username, string password, string role)
        {
            this.username = username;
            this.password = password;
            this.role = role;
        }


        public bool IsManager()
        {
            if (role == "Manager" || role == "manager")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool IsEmployee()
        {
            if (role == "Employee" || role == "employee")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool IsValidPassword(string password)
        {
            return password.All(char.IsDigit);
        }

        public static bool IsValidUsername(string username)
        {
            return username.All(char.IsLetter);
        }


        
    }
}
