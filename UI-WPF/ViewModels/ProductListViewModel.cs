using LukomskiMajorkowski.KeyboardCatalog.INTERFACES;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace LukomskiMajorkowski.KeyboardCatalog.UI_WPF.ViewModels
{
    public class ProductListViewModel : ViewModelBase
    {
        private readonly IDAO _dao;

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

        public ProductListViewModel(IDAO dao)
        {
            _dao = dao;

            Products = new ObservableCollection<IProduct>(_dao.GetAllProducts());

            AddProductCommand = new RelayCommand<object>(_ => OpenAddProductForm());
            EditProductCommand = new RelayCommand<IProduct>(OpenEditProductForm, CanEditProduct);
            DeleteProductCommand = new RelayCommand<IProduct>(DeleteProduct, CanDeleteProduct);
            GoToManufacturerListCommand = new RelayCommand<object>(_ => OpenManufacturerList());
        }

        private void OpenAddProductForm()
        {
            var productForm = new ProductFormWindow(_dao);
            productForm.ShowDialog();
            RefreshProducts();
        }

        private void OpenEditProductForm(IProduct product)
        {
            if (product == null) return;

            var productForm = new ProductFormWindow(_dao, product);
            productForm.ShowDialog();
            RefreshProducts();
        }

        private void DeleteProduct(IProduct product)
        {
            if (product == null) return;

            _dao.DeleteProduct(product.Id);
            RefreshProducts();
        }

        private void RefreshProducts()
        {
            Products.Clear();
            var sortedProducts = _dao.GetAllProducts().OrderBy(p => p.Id); // Sort by ID
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
            var manufacturerListWindow = new ManufacturerListWindow(_dao);
            manufacturerListWindow.Show();
        }
    }
}
