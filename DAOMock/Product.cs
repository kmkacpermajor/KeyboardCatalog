using LukomskiMajorkowski.KeyboardCatalog.CORE;
using LukomskiMajorkowski.KeyboardCatalog.INTERFACES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAOMock
{
    public class Product : IProduct
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public KeyboardType Type { get; set; }
        public IManufacturer Manufacturer { get; set; }
    }
}
