using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App.BL;

namespace App.DL
{
    class ProductsCRUD
    {
        public static void AddProduct(string productName, float productPrice, int productQuantity, int unitsSold)
        {
            BL.Products.products.Add(new Products(productName, productPrice, productQuantity, unitsSold));

        }

        public static void DeleteProduct(string productNameToDelete) 
        {
            Products productToDelete = BL.Products.products.FirstOrDefault(p => p.Name == productNameToDelete);
            if (productToDelete != null)
            {
                BL.Products.products.Remove(productToDelete);
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
            }
        }

        public static void DisplayProducts()
        {
            Console.WriteLine("\nProducts List:");
            Console.WriteLine("Name\t\tPrice\t\tQuantity\tUnits Sold");
            foreach (var product in BL.Products.products)
            {
                Console.WriteLine($"{product.Name}\t\t{product.Price}\t\t{product.Quantity}\t\t{product.UnitsSold}");
            }
        }

        public static void ShowSales()
        {
            foreach (var product in BL.Products.products)
            {
                Console.WriteLine($"Name: {product.Name}\t Units Sold: {product.UnitsSold} ");
            }
        }

    }
}
