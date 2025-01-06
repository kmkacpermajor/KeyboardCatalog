using LukomskiMajorkowski.KeyboardCatalog.BL;
using LukomskiMajorkowski.KeyboardCatalog.INTERFACES;
using System.Configuration;
using System.Data;
using System.Windows;

namespace LukomskiMajorkowski.KeyboardCatalog.UI_WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // Use the factory to create repository instances
            IDAO dao = DAOFactory.CreateDAO();

            // Pass repositories to the main window
            var mainWindow = new ProductListWindow(dao);
            mainWindow.Show();
        }
    }

}
