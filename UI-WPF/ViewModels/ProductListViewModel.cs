using INTERFACES;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace UI_WPF.ViewModels
{
    public class ProductListViewModel : ViewModelBase
    {
        private readonly IProductRepository _productRepository;
        private readonly IManufacturerRepository _manufacturerRepository;

        public ObservableCollection<IProduct> Products { get; set; }

        private IProduct _selectedProduct;
        public IProduct SelectedProduct
        {
            get => _selectedProduct;
            set
            {
                _selectedProduct = value;
                OnPropertyChanged(nameof(SelectedProduct));
                OnPropertyChanged(nameof(IsEditEnabled)); // Notify that IsEditEnabled has changed
                OnPropertyChanged(nameof(IsDeleteEnabled)); // Notify that IsDeleteEnabled has changed
            }
        }

        public ICommand AddProductCommand { get; }
        public ICommand EditProductCommand { get; }
        public ICommand DeleteProductCommand { get; }
        public ICommand GoToManufacturerListCommand { get; }

        public bool IsEditEnabled => SelectedProduct != null; // Inline check for Edit button
        public bool IsDeleteEnabled => SelectedProduct != null; // Inline check for Delete button

        public ProductListViewModel(IProductRepository productRepository,
                                    IManufacturerRepository manufacturerRepository)
        {
            _productRepository = productRepository;
            _manufacturerRepository = manufacturerRepository;

            Products = new ObservableCollection<IProduct>(_productRepository.GetAllProducts());

            AddProductCommand = new RelayCommand<object>(_ => OpenAddProductForm());
            EditProductCommand = new RelayCommand<IProduct>(OpenEditProductForm, CanEditProduct);
            DeleteProductCommand = new RelayCommand<IProduct>(DeleteProduct, CanDeleteProduct);
            GoToManufacturerListCommand = new RelayCommand<object>(_ => OpenManufacturerList());
        }

        private void OpenAddProductForm()
        {
            var productForm = new ProductFormWindow(_productRepository, _manufacturerRepository);
            productForm.ShowDialog();
            RefreshProducts();
        }

        private void OpenEditProductForm(IProduct product)
        {
            if (product == null) return;

            var productForm = new ProductFormWindow(_productRepository, _manufacturerRepository, product);
            productForm.ShowDialog();
            RefreshProducts();
        }

        private void DeleteProduct(IProduct product)
        {
            if (product == null) return;

            _productRepository.DeleteProduct(product.Id);
            RefreshProducts();
        }

        private void RefreshProducts()
        {
            Products.Clear();
            var sortedProducts = _productRepository.GetAllProducts().OrderBy(p => p.Id); // Sort by ID
            foreach (var product in sortedProducts)
            {
                Products.Add(product);
            }
        }

        private bool CanEditProduct(IProduct product)
        {
            return product != null;
        }

        private bool CanDeleteProduct(IProduct product)
        {
            return product != null;
        }

        // Open the Manufacturer List window
        private void OpenManufacturerList()
        {
            var manufacturerListWindow = new ManufacturerListWindow(_manufacturerRepository);
            manufacturerListWindow.Show();
        }
    }
}
