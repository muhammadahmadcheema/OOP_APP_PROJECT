using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.BL
{
    class Products
    {
        public string Name { get; set; }
        public float Price { get; set; }
        public int Quantity { get; set; }

        public int UnitsSold { get; set; }

        public Products(string name, float price, int quantity, int unitsSold)
        {
            this.Name = name;
            this.Price = price;
            this.Quantity = quantity;
            this.UnitsSold = unitsSold;
        }

        public static List<Products> products = new List<Products>();






    }
}
