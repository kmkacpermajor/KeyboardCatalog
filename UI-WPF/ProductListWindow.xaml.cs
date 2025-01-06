using LukomskiMajorkowski.KeyboardCatalog.INTERFACES;
using System.Windows;
using LukomskiMajorkowski.KeyboardCatalog.UI_WPF.ViewModels;

namespace LukomskiMajorkowski.KeyboardCatalog.UI_WPF
{
    public partial class ProductListWindow : Window
    {
        public ProductListWindow(IDAO dao)
        {
            InitializeComponent();
            DataContext = new ProductListViewModel(dao);
        }
    }
}
