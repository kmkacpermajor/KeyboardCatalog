using INTERFACES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace DAO
{
    public class DAOFile : IProductRepository, IManufacturerRepository
    {
        private readonly string _filePathProducts = "products.json";
        private readonly string _filePathManufacturers = "manufacturers.json";
        private List<IProduct> _products;
        private List<IManufacturer> _manufacturers;

        public DAOFile()
        {
            _products = LoadProducts();
            _manufacturers = LoadManufacturers();
        }

        // Product methods
        public IEnumerable<IProduct> GetAllProducts() => _products;

        public IProduct GetProductById(int id) => _products.FirstOrDefault(p => p.Id == id);

        public void Add(IProduct product)
        {
            _products.Add(product);
            SaveProducts();
        }

        public void Update(IProduct product)
        {
            var existingProduct = GetProductById(product.Id);
            if (existingProduct != null)
            {
                _products.Remove(existingProduct);
                _products.Add(product);
                SaveProducts();
            }
        }

        public void DeleteProduct(int id)
        {
            var product = GetProductById(id);
            if (product != null)
            {
                _products.Remove(product);
                SaveProducts();
            }
        }

        // Manufacturer methods
        public IEnumerable<IManufacturer> GetAllManufacturers() => _manufacturers;

        public IManufacturer GetManufacturerById(int id) => _manufacturers.FirstOrDefault(m => m.Id == id);

        public void Add(IManufacturer manufacturer)
        {
            _manufacturers.Add(manufacturer);
            SaveManufacturers();
        }

        public void Update(IManufacturer manufacturer)
        {
            var existingManufacturer = GetManufacturerById(manufacturer.Id);
            if (existingManufacturer != null)
            {
                _manufacturers.Remove(existingManufacturer);
                _manufacturers.Add(manufacturer);
                SaveManufacturers();
            }
        }

        public void DeleteManufacturer(int id)
        {
            var manufacturer = GetManufacturerById(id);
            if (manufacturer != null)
            {
                _manufacturers.Remove(manufacturer);
                SaveManufacturers();
            }
        }

        // File handling for products
        private List<IProduct> LoadProducts()
        {
            if (!File.Exists(_filePathProducts))
                return new List<IProduct>();

            var json = File.ReadAllText(_filePathProducts);
            return JsonSerializer.Deserialize<List<IProduct>>(json) ?? new List<IProduct>();
        }

        private void SaveProducts()
        {
            var json = JsonSerializer.Serialize(_products);
            File.WriteAllText(_filePathProducts, json);
        }

        // File handling for manufacturers
        private List<IManufacturer> LoadManufacturers()
        {
            if (!File.Exists(_filePathManufacturers))
                return new List<IManufacturer>();

            var json = File.ReadAllText(_filePathManufacturers);
            return JsonSerializer.Deserialize<List<IManufacturer>>(json) ?? new List<IManufacturer>();
        }

        private void SaveManufacturers()
        {
            var json = JsonSerializer.Serialize(_manufacturers);
            File.WriteAllText(_filePathManufacturers, json);
        }
    }
}
