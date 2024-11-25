using CORE;
using System.Collections.Generic;

namespace INTERFACES
{
    public interface IDAO
    {
        IEnumerable<IProduct> GetAllProducts();
        IProduct GetProductById(int id);
        void Add(IProduct product);
        void Update(IProduct product);
        void DeleteProduct(int id);

        IEnumerable<IManufacturer> GetAllManufacturers();
        IManufacturer GetManufacturerById(int id);
        void Add(IManufacturer manufacturer);
        void Update(IManufacturer manufacturer);
        void DeleteManufacturer(int id);
    }
}
