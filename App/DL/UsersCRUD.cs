using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App.BL;

namespace App.DL
{
    class UsersCRUD
    {
        public static List<Users> users = new List<Users>();

        public static void AddUser(string username, string password, string role)
        {
            Users newUser = new Users(username, password, role);
            users.Add(newUser);
            WriteData(UI.Interface.path, users);
        }

        public static void ReadData(string path, List<Users> users)
        {
            users.Clear();

            if (File.Exists(path))
            {
                using (StreamReader file = new StreamReader(path))
                {
                    string line;
                    while ((line = file.ReadLine()) != null)
                    {
                        string[] parts = line.Split(',');
                        if (parts.Length >= 3)
                        {
                            users.Add(new Users(parts[0], parts[1], parts[2]));
                        }
                    }
                }
            }
        }

        public static void WriteData(string path, List<Users> users)
        {
            using (StreamWriter file = new StreamWriter(path, false))
            {
                foreach (Users user in users)
                {
                    file.WriteLine($"{user.username},{user.password},{user.role}");
                }
            }
        }


    }
}
