using LukomskiMajorkowski.KeyboardCatalog.INTERFACES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LukomskiMajorkowski.KeyboardCatalog.DAOFile
{
    public class Manufacturer : IManufacturer
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
