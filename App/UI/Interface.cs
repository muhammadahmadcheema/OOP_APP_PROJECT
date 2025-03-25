using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App.BL;

namespace App.UI
{
    class Interface
    {
        public static string path = "D:\\Second Semester\\OOP\\OOP PROJECT\\AppV2\\App\\credentials.txt";

        //START UP INTERFACE
        //public static void StartUp()
        //{
        //    string choice;
        //    ReadData(path, Users.users);
        //    Console.Clear();
        //    choice = Menu();

        //    switch (choice)
        //    {
        //        case "1":
        //            SignUp(path, Users.users);
        //            StartUp();
        //            break;
        //        case "2":
        //            SignIn(Users.users);
        //            StartUp();
        //            break;
        //        case "3":
        //            Console.WriteLine("See you next time!");
        //            Console.Read();
        //            break;
        //        default:
        //            Console.WriteLine("Invalid option. Please try again.");
        //            StartUp();
        //            break;
        //    }
        //}

        //MENU

        public static string Menu()
        {
            string option;
            Console.WriteLine("====BUSINESS BOOK KEEPING APPLICATION====");
            Console.WriteLine("1. Sign Up");
            Console.WriteLine("2. Sign In");
            Console.WriteLine("3. Exit");
            Console.Write("Enter your choice: ");
            option = Console.ReadLine();
            return option;
        }

        //SIGN IN

        public static void SignIn(List<Users> user)
        {
            Console.WriteLine("Please enter your username: ");
            string username = Console.ReadLine();
            Console.WriteLine("Please enter your password: ");
            string password = Console.ReadLine();

            Users foundUser = null;
            for (int i = 0; i < Users.users.Count; i++)
            {
                if (Users.users[i].username == username && Users.users[i].password == password)
                {
                    foundUser = Users.users[i];
                    break;
                }
            }

            if (foundUser != null)
            {
                Console.WriteLine("Correct Details Entered");

                if (foundUser.IsManager())
                {
                    ManagerMenu(Products.products);
                }
                else if (foundUser.IsEmployee())
                {
                    EmployeeMenu();
                }
            }
            else
            {
                Console.WriteLine("Incorrect Details Entered");
            }

            Console.ReadKey();
        }

        //READ DATA

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

        //SIGNUP

        public static void SignUp(string path, List<Users> users)
        {
            Console.WriteLine("Please enter your username: ");
            string username = Console.ReadLine();
            Console.WriteLine("Please enter your password: ");
            string password = Console.ReadLine();
            Console.WriteLine("Please enter your role (Manager/Employee): ");
            string role = Console.ReadLine();

            if (BL.Users.IsValidUsername(username) && BL.Users.IsValidPassword(password))
            {
                if (users.Any(u => u.username == username))
                {
                    Console.WriteLine("Username already exists.");
                }
                else
                {
                    DL.UsersCRUD.AddUser(username, password, role);
                    Console.WriteLine("Signup successful!");
                }
            }
            else
            {
                Console.WriteLine("Invalid username or password format.");
            }

            Console.ReadKey();
        }

        //WRITE DATA

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

        //MANAGER MENU

        public static void ManagerMenu(List<Products> products)
        {
            string option;
            Console.Clear();
            Console.WriteLine("====BUSINESS BOOK KEEPING APPLICATION====");
            Console.WriteLine("1. Add Product & Price.");
            Console.WriteLine("2. Delete Product.");
            Console.WriteLine("3. Update Product Name, Price & Quantity");
            Console.WriteLine("4. View Products, Prices, Quantity");
            Console.WriteLine("5. View Sales");
            Console.WriteLine("6. Sales Comparison with Preceding Year");
            Console.WriteLine("7. Ad Spend & Miscellanous Expenses");
            Console.WriteLine("8. Go Back to Login Page");
            Console.Write("Enter your choice: ");
            option = Console.ReadLine();

            if (option == "1")
            {
                string addMore = "1";
                while (addMore == "1")
                {
                    Console.Write("Enter Product Name: "); //if options wala kaam bhi main mei krna
                    string productName = Console.ReadLine();
                    Console.Write("Enter Product Price: ");
                    float productPrice = float.Parse(Console.ReadLine());
                    Console.Write("Enter Product Quantity: ");
                    int productQuantity = int.Parse(Console.ReadLine());
                    int unitsSold = 0;
                    DL.ProductsCRUD.AddProduct(productName, productPrice, productQuantity, unitsSold); //main mei kro
                    Console.WriteLine("Product added successfully!");
                    Console.WriteLine("Add another product? (1: Yes, 0: No): ");
                    addMore = Console.ReadLine();
                }
                Console.WriteLine("\nPress any key to return to Manager Menu...");
                Console.ReadKey();
                ManagerMenu(products);
            }

            if (option == "2")
            {
                if (products.Count == 0)
                {
                    Console.WriteLine("\nNo products have been added yet.");
                }
                else
                {
                    Console.Write("\nEnter the name of the product to delete: ");
                    string productNameToDelete = Console.ReadLine();
                    DL.ProductsCRUD.DeleteProduct(productNameToDelete);
                }
                Console.WriteLine("\nPress any key to return to Manager Menu...");
                Console.ReadKey();
                ManagerMenu(products);
            }

            if (option == "3")
            {
                if (products.Count == 0)
                {
                    Console.WriteLine("\nNo products have been added yet.");
                }
                else
                {
                    Console.Write("\nEnter the name of the product to update: ");
                    string productNameToUpdate = Console.ReadLine();

                    Products productToUpdate = products.FirstOrDefault(p => p.Name == productNameToUpdate);

                    if (productToUpdate != null)
                    {
                        DL.ProductsCRUD.UpdateProduct(productNameToUpdate);
                        Console.WriteLine($"\nProduct '{productNameToUpdate}' updated successfully!");
                    }
                    else
                    {
                        Console.WriteLine($"\nProduct '{productNameToUpdate}' not found.");
                    }
                }
                Console.WriteLine("\nPress any key to return to Manager Menu...");
                Console.ReadKey();
                ManagerMenu(products);
            }

            if (option == "4")
            {
                if (products.Count == 0)
                {
                    Console.WriteLine("\nNo products have been added yet.");
                }
                else
                {
                    DL.ProductsCRUD.DisplayProducts();
                }

                Console.WriteLine("\nPress any key to return to Manager Menu...");
                Console.ReadKey();
                ManagerMenu(products);
            }

            if (option == "5")
            {
                if (products.Count == 0)
                {
                    Console.WriteLine("\nNo products have been added yet.");
                }
                else
                {
                    Console.WriteLine("\nCurrent Sales:");
                    DL.ProductsCRUD.ShowSales();
                }
                Console.WriteLine("\nPress any key to return to Manager Menu...");
                Console.ReadKey();
                ManagerMenu(products);
            }

            if (option == "6")
            {
                Console.WriteLine("Sales Comparison with Preceding Year");
                Console.WriteLine("\nEnter the sales of Preceding Year: ");
                float salesPrecedingYear = float.Parse(Console.ReadLine());
                Console.WriteLine("Enter the sales of Current Year: ");
                float salesCurrentYear = float.Parse(Console.ReadLine());
                float salesComparison = (salesPrecedingYear - salesCurrentYear) / 100;
                Console.WriteLine($"\nSales Comparison: {salesComparison}%");

                Console.WriteLine("\nPress any key to return to Manager Menu...");
                Console.ReadKey();
                ManagerMenu(products);
            }

            if (option == "7")
            {
                Console.WriteLine("Ad Spend & Miscellanous Expenses");
                Console.WriteLine("Enter the Ad Spend: ");
                float adSpend = float.Parse(Console.ReadLine());
                Console.WriteLine("Enter the Miscellanous Expenses: ");
                float miscExpenses = float.Parse(Console.ReadLine());
                float totalExpenses = adSpend + miscExpenses;
                Console.WriteLine($"\nTotal Expenses: {totalExpenses}");

                Console.WriteLine("\nPress any key to return to Manager Menu...");
                Console.ReadKey();
                ManagerMenu(products);

            }

            if (option == "8")
            {
                Program.StartUp();
            }


        }

        //EMPLOYEE MENU

        public static void EmployeeMenu()
        {
            string option;
            Console.Clear();
            Console.WriteLine("====BUSINESS BOOK KEEPING APPLICATION====");
            Console.WriteLine("1. View Product And Price");
            Console.WriteLine("2. Add Units Sold");
            Console.WriteLine("3. View Sales");
            Console.WriteLine("4. Add Stock");
            Console.WriteLine("5. Update Stock");
            Console.WriteLine("6. View Stock");
            Console.WriteLine("7. View Ad Spend & Miscellanous Expenses");
            Console.WriteLine("8. Go Back to Login Page");
            Console.Write("Enter your choice: ");
            option = Console.ReadLine();
        }





    }
}

