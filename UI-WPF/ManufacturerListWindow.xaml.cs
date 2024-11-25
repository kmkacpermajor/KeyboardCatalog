using INTERFACES;
using System.Windows;
using UI_WPF.ViewModels;

namespace UI_WPF
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
