using INTERFACES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class ProductService
    {
        private readonly IProductRepository _repository;

        public ProductService(IProductRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<IProduct> GetAllProducts() => _repository.GetAllProducts();

        public IProduct GetProductById(int id) => _repository.GetProductById(id);

        public void AddProduct(IProduct product) => _repository.Add(product);

        public void UpdateProduct(IProduct product) => _repository.Update(product);

        public void DeleteProduct(int id) => _repository.DeleteProduct(id);
    }
}
