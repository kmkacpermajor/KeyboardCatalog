﻿using CORE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace INTERFACES
{
    public interface IProduct
    {
        int Id { get; set; }
        string Name { get; set; }
        KeyboardType Type { get; set; }
        IManufacturer Manufacturer { get; set; }
    }
}