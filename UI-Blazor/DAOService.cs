using INTERFACES;
using CORE;
using System.Collections.Generic;
using System.Threading.Tasks;
using BL;

namespace LukomskiMajorkowski.KeyboardCatalog.UI_Blazor
{
    public class DAOService
    {
        private readonly IDAO _dao;

        public DAOService()
        {
            _dao = DAOFactory.CreateDAO();  // Tworzenie instancji DAO za pomocą fabryki
        }

        public IEnumerable<IProduct> GetAllProducts()
        {
            return _dao.GetAllProducts();
        }

        public IProduct GetProductById(int id)
        {
            return _dao.GetProductById(id);
        }

        public void AddProduct(IProduct product)
        {
            _dao.Add(product);
        }

        public void UpdateProduct(IProduct product)
        {
            _dao.Update(product);
        }

        public void DeleteProduct(int id)
        {
            _dao.DeleteProduct(id);
        }

        // Analogicznie dla producentów
        public IEnumerable<IManufacturer> GetAllManufacturers()
        {
            return _dao.GetAllManufacturers();
        }

        public IManufacturer GetManufacturerById(int id)
        {
            return _dao.GetManufacturerById(id);
        }

        public void AddManufacturer(IManufacturer manufacturer)
        {
            _dao.Add(manufacturer);
        }

        public void UpdateManufacturer(IManufacturer manufacturer)
        {
            _dao.Update(manufacturer);
        }

        public void DeleteManufacturer(int id)
        {
            _dao.DeleteManufacturer(id);
        }
    }
}
