using LukomskiMajorkowski.KeyboardCatalog.CORE;
using LukomskiMajorkowski.KeyboardCatalog.INTERFACES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LukomskiMajorkowski.KeyboardCatalog.UI_WPF.Models
{
    public class Product : IProduct
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public KeyboardType Type { get; set; }
        public IManufacturer Manufacturer { get; set; }

        public Product()
        {
        }

        public Product(int id, string name, KeyboardType type, IManufacturer manufacturer)
        {
            Id = id;
            Name = name;
            Type = type;
            Manufacturer = manufacturer;
        }
    }
}
