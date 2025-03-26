using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App.BL;
using App.UI;

namespace App
{
    class Program
    {
        static void Main(string[] args)
        {
            StartUp();
        }

        public static void StartUp()
        {
            string choice;
            DL.UsersCRUD.ReadData(UI.Interface.path, DL.UsersCRUD.users);
            Console.Clear();
            choice = UI.Interface.Menu();

            switch (choice)
            {
                case "1":
                    UI.Interface.SignUp(UI.Interface.path, DL.UsersCRUD.users);
                    StartUp();
                    break;
                case "2":
                    UI.Interface.SignIn(DL.UsersCRUD.users);
                    StartUp();
                    break;
                case "3":
                    Console.WriteLine("See you next time!");
                    Console.Read();
                    break;
                default:
                    Console.WriteLine("Invalid option. Please try again.");
                    StartUp();
                    break;
            }
        }
    }
}
