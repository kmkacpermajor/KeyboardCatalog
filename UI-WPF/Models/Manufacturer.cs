using INTERFACES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI_WPF.Models
{
    public class Manufacturer : IManufacturer
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public Manufacturer()
        {
        }

        public Manufacturer(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
