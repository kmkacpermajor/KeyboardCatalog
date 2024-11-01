using INTERFACES;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MvvmHelpers;

namespace UI
{
    public class MainViewModel : BaseViewModel
    {
        private readonly IProductRepository _productRepository;
        public ObservableCollection<ProductViewModel> Products { get; private set; }

        public MainViewModel(IProductRepository productRepository)
        {
            _productRepository = productRepository;
            Products = new ObservableCollection<ProductViewModel>(_productRepository.GetAll().Select(p => new ProductViewModel(p)));
        }

        public void AddProduct(ProductViewModel product)
        {
            _productRepository.Add(product.ToModel());
            Products.Add(product);
        }

        public void UpdateProduct(ProductViewModel product)
        {
            _productRepository.Update(product.ToModel());
            var existingProduct = Products.FirstOrDefault(p => p.Id == product.Id);
            if (existingProduct != null)
            {
                Products[Products.IndexOf(existingProduct)] = product;
            }
        }

        public void DeleteProduct(ProductViewModel product)
        {
            _productRepository.Delete(product.Id);
            Products.Remove(product);
        }

        // Add methods for search/filter as needed
    }
}
