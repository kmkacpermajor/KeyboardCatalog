using INTERFACES;
using System.Windows;
using UI_WPF.ViewModels;

namespace UI_WPF
{
    public partial class ProductListWindow : Window
    {
        public ProductListWindow(IProductRepository productRepository, IManufacturerRepository manufacturerRepository)
        {
            InitializeComponent();
            DataContext = new ProductListViewModel(productRepository, manufacturerRepository);
        }
    }
}
