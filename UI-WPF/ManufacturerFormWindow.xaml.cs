using LukomskiMajorkowski.KeyboardCatalog.INTERFACES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using LukomskiMajorkowski.KeyboardCatalog.UI_WPF.ViewModels;

namespace LukomskiMajorkowski.KeyboardCatalog.UI_WPF
{
    /// <summary>
    /// Interaction logic for ManufacturerFormWindow.xaml
    /// </summary>
    public partial class ManufacturerFormWindow : Window
    {
        public ManufacturerFormViewModel ViewModel { get; private set; }

        public ManufacturerFormWindow(IDAO dao,
                                       IManufacturer manufacturer = null)
        {
            InitializeComponent();
            ViewModel = new ManufacturerFormViewModel(dao, this, manufacturer);
            DataContext = ViewModel;
        }
    }

}
