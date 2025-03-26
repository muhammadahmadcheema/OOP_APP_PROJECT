using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using App.BL;
using MySqlX.XDevAPI;

namespace App.DL
{
    class ProductsCRUD
    {

        public static List<Products> products = new List<Products>();


        public static void AddProduct(string productName, float productPrice, int productQuantity, int unitsSold)
        {
            BL.Products.products.Add(new Products(productName, productPrice, productQuantity, unitsSold));

            string query = $"INSERT INTO products VALUES('{productName}', '{productPrice}', '{productQuantity}')";
            DatabaseHelper.Instance.Update(query);

        }

        public static void DeleteProduct(string productNameToDelete) 
        {
            Products productToDelete = BL.Products.products.FirstOrDefault(p => p.Name == productNameToDelete);
            if (productToDelete != null)
            {
                BL.Products.products.Remove(productToDelete);
                string query = $"DELETE FROM products WHERE product_name = '{productNameToDelete}'";
                DatabaseHelper.Instance.Update(query);
                Console.WriteLine($"\nProduct '{productNameToDelete}' deleted successfully!");
            }
            else
            {
                Console.WriteLine($"\nProduct '{productNameToDelete}' not found.");
            }
        }

        public static void UpdateProduct(string productNameToUpdate)
        {
            Products productToUpdate = BL.Products.products.FirstOrDefault(p => p.Name == productNameToUpdate);

            if (productToUpdate != null)
            {
                Console.Write("Enter new Product Name (or press Enter to keep the same): ");
                string newProductName = Console.ReadLine();
                if (!string.IsNullOrEmpty(newProductName))
                {
                    productToUpdate.Name = newProductName;
                }

                Console.Write("Enter new Product Price (or press Enter to keep the same): ");
                string priceInput = Console.ReadLine();
                if (!string.IsNullOrEmpty(priceInput))
                {
                    productToUpdate.Price = float.Parse(priceInput);
                }

                Console.Write("Enter new Product Quantity (or press Enter to keep the same): ");
                string quantityInput = Console.ReadLine();
                if (!string.IsNullOrEmpty(quantityInput))
                {
                    productToUpdate.Quantity = int.Parse(quantityInput);
                }

                string query = $"UPDATE products SET product_name = '{newProductName}', price = '{priceInput}' , quantity = '{quantityInput}'  WHERE product_name = '{productNameToUpdate}'";
                DatabaseHelper.Instance.Update(query);
            }
        }

        public static void DisplayProducts()
        {
            string query = "SELECT product_name, price, quantity FROM products";
            var reader = DatabaseHelper.Instance.getData(query);

            Console.WriteLine("\nProducts List:");
            Console.WriteLine("Name\t\tPrice\t\tQuantity");

            while (reader.Read())
            {
                string productName = reader["product_name"].ToString();
                decimal price = Convert.ToDecimal(reader["price"]);
                int quantity = Convert.ToInt32(reader["quantity"]);

                Console.WriteLine($"{productName}\t\t{price}\t\t{quantity}");
            }
        }

        public static void ShowSales()
        {
            string query = "SELECT product_name, units_sold FROM products";
            var reader = DatabaseHelper.Instance.getData(query);

            while (reader.Read())
            {
                Console.WriteLine($"Product Name: {reader["product_name"]}\t Units Sold: {reader["units_sold"]}");
            }
        }

        public static void AddSales(string productName)
        {
            string checkQuery = $"SELECT product_name FROM products WHERE product_name = '{productName}'";
            var reader = DatabaseHelper.Instance.getData(checkQuery);

            if (reader.Read())
            {
                Console.Write("Enter Units Sold for the Product: ");
                string input = Console.ReadLine();

                if (!string.IsNullOrEmpty(input) && int.TryParse(input, out int unitsSold))
                {
                    string updateQuery = $"UPDATE products SET units_sold = {unitsSold} WHERE product_name = '{productName}'";
                    DatabaseHelper.Instance.Update(updateQuery);
                    Console.WriteLine($"Units sold added successfully for product: {productName}");
                }
                else
                {
                    Console.WriteLine("Invalid input! Please enter a valid number.");
                }
            }
            else
            {
                Console.WriteLine($"Product '{productName}' not found.");
            }
        }

    }
}
