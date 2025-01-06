using LukomskiMajorkowski.KeyboardCatalog.CORE;
using LukomskiMajorkowski.KeyboardCatalog.INTERFACES;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LukomskiMajorkowski.KeyboardCatalog.DAOSQL
{
    public class Product : IProduct
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public KeyboardType Type { get; set; }
        [NotMapped]
        public IManufacturer Manufacturer { get; set; }

        public Manufacturer ManufacturerEntity
        {
            get => (Manufacturer)Manufacturer;
            set => Manufacturer = value;
        }

    }
}
