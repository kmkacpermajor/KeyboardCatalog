using LukomskiMajorkowski.KeyboardCatalog.INTERFACES;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace LukomskiMajorkowski.KeyboardCatalog.UI_WPF.ViewModels
{
    public class ProductListViewModel : ViewModelBase
    {
        private readonly IDAO _dao;

        public ObservableCollection<IProduct> Products { get; set; }

        private ObservableCollection<IProduct> _filteredProducts;
        public ObservableCollection<IProduct> FilteredProducts
        {
            get => _filteredProducts;
            set
            {
                _filteredProducts = value;
                OnPropertyChanged(nameof(FilteredProducts));
            }
        }

        private string _searchText;
        public string SearchText
        {
            get => _searchText;
            set
            {
                _searchText = value;
                OnPropertyChanged(nameof(SearchText));
                FilterProducts();
            }
        }

        private IProduct _selectedProduct;
        public IProduct SelectedProduct
        {
            get => _selectedProduct;
            set
            {
                _selectedProduct = value;
                OnPropertyChanged(nameof(SelectedProduct));
                OnPropertyChanged(nameof(IsEditEnabled));
                OnPropertyChanged(nameof(IsDeleteEnabled));
            }
        }

        public ICommand AddProductCommand { get; }
        public ICommand EditProductCommand { get; }
        public ICommand DeleteProductCommand { get; }
        public ICommand GoToManufacturerListCommand { get; }

        public bool IsEditEnabled => SelectedProduct != null;
        public bool IsDeleteEnabled => SelectedProduct != null;

        public ProductListViewModel(IDAO dao)
        {
            _dao = dao;

            Products = new ObservableCollection<IProduct>(_dao.GetAllProducts());
            FilteredProducts = new ObservableCollection<IProduct>(Products);

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
            var sortedProducts = _dao.GetAllProducts().OrderBy(p => p.Id);
            foreach (var product in sortedProducts)
            {
                Products.Add(product);
            }
            FilterProducts();
        }

        private void FilterProducts()
        {
            if (string.IsNullOrWhiteSpace(SearchText))
            {
                FilteredProducts = new ObservableCollection<IProduct>(Products);
            }
            else
            {
                var lowerSearchText = SearchText.ToLower();
                FilteredProducts = new ObservableCollection<IProduct>(
                    Products.Where(p => p.Id.ToString().Contains(lowerSearchText) ||
                                        p.Name.ToLower().Contains(lowerSearchText) ||
                                        p.Type.ToString().ToLower().Contains(lowerSearchText) ||
                                        p.Manufacturer?.Name.ToLower().Contains(lowerSearchText) == true));
            }
        }

        private bool CanEditProduct(IProduct product) => product != null;

        private bool CanDeleteProduct(IProduct product) => product != null;

        private void OpenManufacturerList()
        {
            var manufacturerListWindow = new ManufacturerListWindow(_dao);
            manufacturerListWindow.Show();
        }
    }
}
