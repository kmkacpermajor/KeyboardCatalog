using INTERFACES;
using System.Windows;
using UI_WPF.ViewModels;

namespace UI_WPF
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
