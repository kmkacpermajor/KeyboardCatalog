using LukomskiMajorkowski.KeyboardCatalog.CORE;
using LukomskiMajorkowski.KeyboardCatalog.INTERFACES;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DAOMock
{
    public class DAOMock : IDAO
    {
        private List<IProduct> _products = new List<IProduct>();
        private List<IManufacturer> _manufacturers = new List<IManufacturer>();
        private int _nextProductId = 1; // Start from 1 for unique IDs
        private int _nextManufacturerId = 1; // For manufacturers if needed

        public DAOMock()
        {
            // Sample data for manufacturers
            _manufacturers.Add(new Manufacturer { Id = _nextManufacturerId++, Name = "Logitech" });
            _manufacturers.Add(new Manufacturer { Id = _nextManufacturerId++, Name = "Corsair" });

            // Sample data for products
            _products.Add(new Product
            {
                Id = _nextProductId++, // Increment the ID for each product
                Name = "Logitech G Pro",
                Type = KeyboardType.Mechanical, // Assuming Type is a string
                Manufacturer = GetManufacturerById(1) // Setting manufacturer as the first one
            });

            _products.Add(new Product
            {
                Id = _nextProductId++,
                Name = "Corsair K95",
                Type = KeyboardType.Mechanical,
                Manufacturer = GetManufacturerById(2) // Setting manufacturer as the second one
            });
        }

        public IEnumerable<IProduct> GetAllProducts() => _products;

        public IProduct GetProductById(int id) => _products.FirstOrDefault(p => p.Id == id);

        public void Add(IProduct product)
        {
            product.Id = _nextProductId++; // Assign a new ID before adding
            _products.Add(product);
        }

        public void Update(IProduct product)
        {
            var existingProduct = GetProductById(product.Id);
            if (existingProduct != null)
            {
                _products.Remove(existingProduct);
                _products.Add(product);
            }
        }

        public void DeleteProduct(int id)
        {
            var product = GetProductById(id);
            if (product != null)
            {
                _products.Remove(product);
            }
        }

        // Manufacturer methods
        public IEnumerable<IManufacturer> GetAllManufacturers() => _manufacturers;

        public IManufacturer GetManufacturerById(int id) => _manufacturers.FirstOrDefault(m => m.Id == id);

        public void Add(IManufacturer manufacturer)
        {
            manufacturer.Id = _nextManufacturerId++; // Assign a new ID before adding
            _manufacturers.Add(manufacturer);
        }

        public void Update(IManufacturer manufacturer)
        {
            var existingManufacturer = GetManufacturerById(manufacturer.Id);
            if (existingManufacturer != null)
            {
                _manufacturers.Remove(existingManufacturer);
                _manufacturers.Add(manufacturer);
            }
        }

        public void DeleteManufacturer(int id)
        {
            var manufacturer = GetManufacturerById(id);
            if (manufacturer != null)
            {
                _manufacturers.Remove(manufacturer);
            }
        }
    }
}
