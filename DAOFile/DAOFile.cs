using CORE;
using INTERFACES;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace DAOFile
{
    public class DAOFile : IDAO, IDisposable
    {
        private readonly string _filePathProducts = "products.json";
        private readonly string _filePathManufacturers = "manufacturers.json";
        private List<IProduct> _products;
        private List<IManufacturer> _manufacturers;

        private bool _disposed = false;

        // Counters for assigning IDs
        private int _nextProductId;
        private int _nextManufacturerId;

        public DAOFile()
        {
            _manufacturers = LoadManufacturers();
            _products = LoadProducts();

            // Set counters based on current data
            _nextProductId = _products.Any() ? _products.Max(p => p.Id) + 1 : 1;
            _nextManufacturerId = _manufacturers.Any() ? _manufacturers.Max(m => m.Id) + 1 : 1;
        }

        // Product methods
        public IEnumerable<IProduct> GetAllProducts() => _products;

        public IProduct GetProductById(int id) => _products.FirstOrDefault(p => p.Id == id);

        public void Add(IProduct product)
        {
            product.Id = _nextProductId++;
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
            manufacturer.Id = _nextManufacturerId++;
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
            var productDataList = JsonSerializer.Deserialize<List<Dictionary<string, JsonElement>>>(json) ?? new List<Dictionary<string, JsonElement>>();

            var products = productDataList.Select(pd => new Product
            {
                Id = pd["Id"].GetInt32(),
                Name = pd["Name"].GetString(),
                Type = Enum.Parse<KeyboardType>(pd["Type"].GetString()),
                Manufacturer = GetManufacturerById(pd["ManufacturerId"].GetInt32())
            }).Cast<IProduct>().ToList();

            return products;
        }

        private void SaveProducts()
        {
            var productDataList = _products.Select(p => new Dictionary<string, object>
            {
                { "Id", p.Id },
                { "Name", p.Name },
                { "Type", p.Type.ToString() },
                { "ManufacturerId", p.Manufacturer.Id }
            }).ToList();

            var json = JsonSerializer.Serialize(productDataList);
            File.WriteAllText(_filePathProducts, json);
        }

        // File handling for manufacturers
        private List<IManufacturer> LoadManufacturers()
        {
            if (!File.Exists(_filePathManufacturers))
                return new List<IManufacturer>();

            var json = File.ReadAllText(_filePathManufacturers);
            var manufacturerDataList = JsonSerializer.Deserialize<List<Dictionary<string, JsonElement>>>(json) ?? new List<Dictionary<string, JsonElement>>();

            var manufacturers = manufacturerDataList.Select(md => new Manufacturer
            {
                Id = md["Id"].GetInt32(),
                Name = md["Name"].GetString()
            }).Cast<IManufacturer>().ToList();

            return manufacturers;
        }

        private void SaveManufacturers()
        {
            var json = JsonSerializer.Serialize(_manufacturers);
            File.WriteAllText(_filePathManufacturers, json);
        }

        // Dispose method to save changes before object is collected
        public void Dispose()
        {
            if (!_disposed)
            {
                SaveManufacturers();
                SaveProducts();
                _disposed = true;
            }
            GC.SuppressFinalize(this);
        }

        // Finalizer
        ~DAOFile()
        {
            Dispose();
        }
    }
}
