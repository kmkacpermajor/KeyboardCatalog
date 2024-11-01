using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace INTERFACES
{
    public interface IManufacturerRepository
    {
        IEnumerable<IManufacturer> GetAllManufacturers();
        IManufacturer GetManufacturerById(int id);
        void Add(IManufacturer manufacturer);
        void Update(IManufacturer manufacturer);
        void DeleteManufacturer(int id);
    }
}
