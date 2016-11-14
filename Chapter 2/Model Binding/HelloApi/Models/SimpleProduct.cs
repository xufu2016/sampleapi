using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Web;

namespace HelloApi.Models
{
    [TypeConverter(typeof(SimpleProductConverter))]
    public class SimpleProduct
    {
        public string Name { get; set; }
        public double Price { get; set; }

        public static bool TryParse(string s, out SimpleProduct result)
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
                result = new SimpleProduct { Name = parts[0], Price = price };
                return true;
            }
            return false;
        }
    }

    public class SimpleProductConverter: TypeConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            if (sourceType == typeof(string))
            {
                return true;
            }
            return base.CanConvertFrom(context, sourceType);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            if(value is string)
            {
                SimpleProduct product;
                if(SimpleProduct.TryParse((string)value, out product))
                {
                    return product;
                }
            }
            return base.ConvertFrom(context, culture, value);
        }
    }
}