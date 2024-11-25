using INTERFACES;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace DAOSQL
{
    public class DAOSQL : DbContext, IDAO
    {
        private readonly DbSet<Product> _products;
        private readonly DbSet<Manufacturer> _manufacturers;

        public DAOSQL()
        {
            _products = Set<Product>();
            _manufacturers = Set<Manufacturer>();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string path = @"C:\tmp\Keyboards.db";
            optionsBuilder.UseSqlite($"Filename={path}");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
                .Ignore(p => p.Manufacturer)
                .HasOne(typeof(Manufacturer), nameof(Product.ManufacturerEntity))
                .WithMany()
                .HasForeignKey("ManufacturerId");
        }

        // IDAO Methods
        public IEnumerable<IProduct> GetAllProducts()
        {
            return _products.Include(p => p.ManufacturerEntity).ToList();
        }

        public IProduct GetProductById(int id)
        {
            return _products.Include(p => p.ManufacturerEntity).FirstOrDefault(p => p.Id == id);
        }

        public void DeleteProduct(int id)
        {
            var product = _products.Find(id);
            if (product != null)
            {
                _products.Remove(product);
                SaveChanges();
            }
        }

        public IEnumerable<IManufacturer> GetAllManufacturers()
        {
            return _manufacturers.ToList();
        }

        public IManufacturer GetManufacturerById(int id)
        {
            return _manufacturers.Find(id);
        }

        public void Add(IManufacturer manufacturer)
        {
            _manufacturers.Add(MapToEntity(manufacturer));
            SaveChanges();
        }

        public void Add(IProduct product)
        {
            // Znajdź istniejącego producenta po Id
            var manufacturer = _manufacturers.Find(product.Manufacturer.Id);

            // Stwórz produkt
            var productEntity = new Product
            {
                Id = product.Id,
                Name = product.Name,
                ManufacturerEntity = manufacturer // Przypisz istniejącego producenta
            };

            // Dodaj produkt
            _products.Add(productEntity);
            SaveChanges();
        }



        public void Update(IProduct product)
        {
            var existingProduct = _products.Include(p => p.ManufacturerEntity).FirstOrDefault(p => p.Id == product.Id);

            // Pobierz istniejącego producenta
            var manufacturer = _manufacturers.Find(product.Manufacturer.Id);

            // Zaktualizuj dane produktu
            existingProduct.Name = product.Name;
            existingProduct.ManufacturerEntity = manufacturer; // Przypisz producenta po referencji

            SaveChanges();
        }




        public void DeleteManufacturer(int id)
        {
            var manufacturer = _manufacturers.Find(id);
            if (manufacturer != null)
            {
                _manufacturers.Remove(manufacturer);
                SaveChanges();
            }
        }

        public void Update(IManufacturer manufacturer)
        {
            var existingManufacturer = _manufacturers.Find(manufacturer.Id);
            if (existingManufacturer != null)
            {
                // Update only the properties that need modification
                existingManufacturer.Name = manufacturer.Name;

                // No need to call Add, simply mark the context as modified
                SaveChanges();
            }
        }


        // Mapping Methods
        private Product MapToEntity(IProduct product)
        {
            return new Product
            {
                Id = product.Id,
                Name = product.Name,
                ManufacturerEntity = MapToEntity(product.Manufacturer)
            };
        }


        private Manufacturer MapToEntity(IManufacturer manufacturer)
        {
            return new Manufacturer
            {
                Id = manufacturer.Id,
                Name = manufacturer.Name
            };
        }
    }
}
