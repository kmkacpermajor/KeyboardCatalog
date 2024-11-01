using CORE;
using INTERFACES;
using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI
{
    public class ProductViewModel : BaseViewModel
    {
        private IProduct _product;

        public int Id => _product.Id;
        public string Name
        {
            get => _product.Name;
            set { _product.Name = value; OnPropertyChanged(); }
        }
        public string Manufacturer
        {
            get => _product.Manufacturer;
            set { _product.Manufacturer = value; OnPropertyChanged(); }
        }
        public KeyboardType Type
        {
            get => _product.Type;
            set { _product.Type = value; OnPropertyChanged(); }
        }

        // TODO: add price

        public ProductViewModel(IProduct product)
        {
            _product = product;
        }

        public IProduct ToModel() => _product;
    }
}
