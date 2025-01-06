using LukomskiMajorkowski.KeyboardCatalog.INTERFACES;
using System.Windows;
using LukomskiMajorkowski.KeyboardCatalog.UI_WPF.ViewModels;

namespace LukomskiMajorkowski.KeyboardCatalog.UI_WPF
{
    public partial class ManufacturerListWindow : Window
    {
        public ManufacturerListWindow(IDAO dao)
        {
            InitializeComponent();
            DataContext = new ManufacturerListViewModel(dao);
        }
    }
}
