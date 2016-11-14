using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HelloApi.Models
{
    //[ModelBinder(typeof(ProductModelBinder))]
    public class Product
    {
        public string Name { get; set; }
        public double Price { get; set; }

        public static bool TryParse(string s, out Product result)
        {
            result = null;

            var parts = s.Split(',');
            if (parts.Length != 2)
            {
                return false;
            }

            double price;
            if (double.TryParse(parts[1], out price))
            {
                result = new Product { Name = parts[0], Price = price };
                return true;
            }
            return false;
        }
    }
}